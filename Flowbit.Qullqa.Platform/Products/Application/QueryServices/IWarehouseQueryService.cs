using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;

namespace Qullqa.Platform.v2.Products.Application.QueryServices;

public interface IWarehouseQueryService
{
    Task<IEnumerable<Warehouse>> Handle(GetAllWarehousesByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<Warehouse?> Handle(GetWarehouseByIdQuery query, CancellationToken cancellationToken);
}
