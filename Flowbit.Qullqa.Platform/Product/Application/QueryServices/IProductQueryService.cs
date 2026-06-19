using Qullqa.Platform.Product.Domain.Model.Aggregates;
using Qullqa.Platform.Product.Domain.Model.Queries;

namespace Qullqa.Platform.Product.Application.QueryServices;

public interface IProductQueryService
{
    Task<Domain.Model.Aggregates.Product?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Domain.Model.Aggregates.Product>> Handle(GetAllProductsByBusinessQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<InventoryItem>> Handle(GetInventoryByBusinessQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<StockMovement>> Handle(GetStockMovementsByProductQuery query, CancellationToken cancellationToken);
}
