using Flowbit.Qullqa.Platform.Products.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Products.Application.QueryServices;

public interface IStockMovementQueryService
{
    Task<IEnumerable<StockMovement>> Handle(GetAllStockMovementsByBusinessIdQuery query, CancellationToken cancellationToken);
}
