using Cortex.Mediator;
using Microsoft.Extensions.Localization;
using Qullqa.Platform.v2.Products.Application.CommandServices;
using Qullqa.Platform.v2.Products.Domain.Model.Commands;
using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Domain.Model.Errors;
using Qullqa.Platform.v2.Products.Domain.Model.Events;
using Qullqa.Platform.v2.Products.Domain.Repositories;
using Qullqa.Platform.v2.Products.Resources;
using Qullqa.Platform.v2.Shared.Application.Model;
using Qullqa.Platform.v2.Shared.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Application.Internal.CommandServices;

/// <summary>
///     Handles inventory mutations — the operations that change how much
///     stock a product has, always leaving a StockMovement audit trail.
/// </summary>
public class InventoryCommandService(
    IInventoryItemRepository inventoryItemRepository,
    IStockMovementRepository stockMovementRepository,
    IBatchRepository batchRepository,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IMediator mediator,
    IStringLocalizer<ProductMessages> localizer)
    : IInventoryCommandService
{
    /// <summary>
    ///     Sums the quantity into the InventoryItem for (ProductId, WarehouseId)
    ///     when it already exists, or creates a new one otherwise — modeling
    ///     the real N:M relation (architecture doc §8.1): a product can now
    ///     have independent stock per warehouse, rather than the frontend's
    ///     original 1:1 model where choosing a different warehouse silently
    ///     moved the product. Always records a StockMovement.
    /// </summary>
    public async Task<Result<InventoryItem>> Handle(RegisterStockIntakeCommand command, CancellationToken cancellationToken)
    {
        if (command.Quantity <= 0)
            return Result<InventoryItem>.Failure(ProductError.InvalidQuantity, localizer[nameof(ProductError.InvalidQuantity)]);

        var existingItem = await inventoryItemRepository.FindByProductAndWarehouseAsync(command.ProductId,
            command.WarehouseId, cancellationToken);

        InventoryItem item;
        if (existingItem != null)
        {
            existingItem.AddStock(command.Quantity);
            if (command.MinimumStock.HasValue) existingItem.UpdateMinimumStock(command.MinimumStock.Value);
            inventoryItemRepository.Update(existingItem);
            item = existingItem;
        }
        else
        {
            item = new InventoryItem(command.ProductId, command.WarehouseId, command.BusinessId, command.Quantity,
                command.MinimumStock ?? 0);
            await inventoryItemRepository.AddAsync(item, cancellationToken);
        }

        await stockMovementRepository.AddAsync(
            new StockMovement(command.ProductId, command.BusinessId, command.WarehouseId, command.Quantity,
                StockMovementType.Intake, command.Supplier ?? string.Empty, command.Note ?? string.Empty),
            cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);

        await PublishStockLevelChangedEvent(item, cancellationToken);

        return Result<InventoryItem>.Success(item);
    }

    /// <summary>
    ///     Decrements inventory after a confirmed sale. Not exposed as its own
    ///     REST endpoint — called via IProductContextFacade by Sales &amp; POS
    ///     (a future phase).
    ///
    ///     KNOWN LIMITATION: takes no WarehouseId (matching Sales' current,
    ///     not-yet-built contract), so it decrements the first InventoryItem
    ///     found for this product with any stock. Correct for the common case
    ///     (one warehouse per product); once a business genuinely splits stock
    ///     across warehouses, Sales will need to pass an explicit WarehouseId.
    /// </summary>
    public async Task<Result<InventoryItem>> Handle(RegisterStockSaleCommand command, CancellationToken cancellationToken)
    {
        if (command.Quantity <= 0)
            return Result<InventoryItem>.Failure(ProductError.InvalidQuantity, localizer[nameof(ProductError.InvalidQuantity)]);

        var items = await inventoryItemRepository.FindAllByProductIdAsync(command.ProductId, cancellationToken);
        var item = items.FirstOrDefault(candidate => candidate.StockUnit > 0);

        if (item == null)
            return Result<InventoryItem>.Failure(ProductError.InsufficientStock, localizer[nameof(ProductError.InsufficientStock)]);

        item.RemoveStock(command.Quantity);
        inventoryItemRepository.Update(item);

        await stockMovementRepository.AddAsync(
            new StockMovement(command.ProductId, command.BusinessId, item.WarehouseId, command.Quantity,
                StockMovementType.Sale, string.Empty, string.Empty),
            cancellationToken);

        await unitOfWork.CompleteAsync(cancellationToken);

        await PublishStockLevelChangedEvent(item, cancellationToken);

        return Result<InventoryItem>.Success(item);
    }

    /// <summary>
    ///     KNOWN LIMITATION: same as RegisterStockSale — operates on the
    ///     first InventoryItem found for the product until a real
    ///     multi-warehouse UI needs to target a specific one.
    /// </summary>
    public async Task<Result<InventoryItem>> Handle(UpdateMinimumStockCommand command, CancellationToken cancellationToken)
    {
        var items = await inventoryItemRepository.FindAllByProductIdAsync(command.ProductId, cancellationToken);
        var item = items.FirstOrDefault();
        if (item == null)
            return Result<InventoryItem>.Failure(ProductError.InventoryItemNotFound,
                localizer[nameof(ProductError.InventoryItemNotFound)]);

        item.UpdateMinimumStock(command.MinimumStock);
        inventoryItemRepository.Update(item);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<InventoryItem>.Success(item);
    }

    /// <summary>
    ///     The product form only captures one expiration date per product (no
    ///     batch selector UI), so if the product already has an ACTIVE batch,
    ///     it's updated in place instead of piling up batches — otherwise
    ///     re-editing a product would keep creating new batches and the
    ///     "nearest expiration" calculation would keep surfacing the oldest
    ///     one instead of what the user just entered.
    /// </summary>
    public async Task<Result<Batch>> Handle(CreateOrUpdateBatchCommand command, CancellationToken cancellationToken)
    {
        var existingBatch = await batchRepository.FindActiveByProductIdAsync(command.ProductId, cancellationToken);

        Batch batch;
        if (existingBatch != null)
        {
            existingBatch.UpdateDetails(command.Expiration, command.PurchasePrice, command.InventoryId);
            batchRepository.Update(existingBatch);
            batch = existingBatch;
        }
        else
        {
            batch = new Batch(command.ProductId, command.BusinessId, command.Expiration, command.PurchasePrice,
                command.InventoryId);
            await batchRepository.AddAsync(batch, cancellationToken);
        }

        await unitOfWork.CompleteAsync(cancellationToken);

        var product = await productRepository.FindByIdAsync(batch.ProductId, cancellationToken);
        await mediator.PublishAsync(
            new BatchRegisteredEvent(batch.Id, batch.ProductId, product?.Name ?? string.Empty, batch.BusinessId, batch.Expiration),
            cancellationToken);

        return Result<Batch>.Success(batch);
    }

    private async Task PublishStockLevelChangedEvent(InventoryItem item, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindByIdAsync(item.ProductId, cancellationToken);
        await mediator.PublishAsync(
            new StockLevelChangedEvent(item.ProductId, product?.Name ?? string.Empty, item.WarehouseId, item.BusinessId,
                item.StockUnit, item.MinimumStock),
            cancellationToken);
    }
}
