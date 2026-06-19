using Qullqa.Platform.Product.Application.CommandServices;
using Qullqa.Platform.Product.Domain.Model;
using Qullqa.Platform.Product.Domain.Model.Aggregates;
using Qullqa.Platform.Product.Domain.Model.Commands;
using Qullqa.Platform.Product.Domain.Repositories;
using Qullqa.Platform.Shared.Application.Model;
using Qullqa.Platform.Shared.Domain.Repositories;

namespace Qullqa.Platform.Product.Application.Internal.CommandServices;

public class ProductCommandService(
    IProductRepository productRepository,
    IInventoryItemRepository inventoryItemRepository,
    IStockMovementRepository stockMovementRepository,
    IUnitOfWork unitOfWork) : IProductCommandService
{
    public async Task<Result<Domain.Model.Aggregates.Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Domain.Model.Aggregates.Product(command.BusinessId, command.Name, command.Description, command.Category, command.BasePrice);
        await productRepository.AddAsync(product, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Domain.Model.Aggregates.Product>.Success(product);
    }

    public async Task<Result<Domain.Model.Aggregates.Product>> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await productRepository.FindByIdAsync(command.Id, cancellationToken);
        if (product == null) return Result<Domain.Model.Aggregates.Product>.Failure(ProductError.ProductNotFound, $"Product with id {command.Id} not found.");
        product.Update(command.Name, command.Description, command.Category, command.BasePrice, command.Status);
        productRepository.Update(product);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Domain.Model.Aggregates.Product>.Success(product);
    }

    public async Task<Result<StockMovement>> Handle(RegisterStockMovementCommand command, CancellationToken cancellationToken)
    {
        var movement = new StockMovement(command.ProductId, command.BusinessId, command.Quantity, command.Type);
        await stockMovementRepository.AddAsync(movement, cancellationToken);

        var inventoryItem = await inventoryItemRepository.FindByProductAndBusinessAsync(command.ProductId, command.BusinessId, cancellationToken);
        if (inventoryItem != null)
        {
            var delta = command.Type == Domain.Model.Enums.MovementType.Sale ? -command.Quantity : command.Quantity;
            inventoryItem.UpdateStock(delta);
            inventoryItemRepository.Update(inventoryItem);
        }

        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<StockMovement>.Success(movement);
    }
}
