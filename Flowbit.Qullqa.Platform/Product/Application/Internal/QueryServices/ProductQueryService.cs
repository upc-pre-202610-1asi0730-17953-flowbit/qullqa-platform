using Qullqa.Platform.Product.Application.QueryServices;
using Qullqa.Platform.Product.Domain.Model.Aggregates;
using Qullqa.Platform.Product.Domain.Model.Queries;
using Qullqa.Platform.Product.Domain.Repositories;

namespace Qullqa.Platform.Product.Application.Internal.QueryServices;

public class ProductQueryService(
    IProductRepository productRepository,
    IInventoryItemRepository inventoryItemRepository,
    IStockMovementRepository stockMovementRepository) : IProductQueryService
{
    public async Task<Domain.Model.Aggregates.Product?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        => await productRepository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<Domain.Model.Aggregates.Product>> Handle(GetAllProductsByBusinessQuery query, CancellationToken cancellationToken)
        => await productRepository.FindByBusinessIdAsync(query.BusinessId, cancellationToken);

    public async Task<IEnumerable<InventoryItem>> Handle(GetInventoryByBusinessQuery query, CancellationToken cancellationToken)
        => await inventoryItemRepository.FindByBusinessIdAsync(query.BusinessId, cancellationToken);

    public async Task<IEnumerable<StockMovement>> Handle(GetStockMovementsByProductQuery query, CancellationToken cancellationToken)
        => await stockMovementRepository.FindByProductAndBusinessAsync(query.ProductId, query.BusinessId, cancellationToken);
}
