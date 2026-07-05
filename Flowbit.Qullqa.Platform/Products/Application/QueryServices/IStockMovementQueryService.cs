using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;

namespace Qullqa.Platform.v2.Products.Application.QueryServices;

public interface IStockMovementQueryService
{
    Task<IEnumerable<StockMovement>> Handle(GetAllStockMovementsByBusinessIdQuery query, CancellationToken cancellationToken);
}
