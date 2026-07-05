using Qullqa.Platform.v2.Products.Application.QueryServices;
using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;
using Qullqa.Platform.v2.Products.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Application.Internal.QueryServices;

public class WarehouseQueryService(IWarehouseRepository warehouseRepository) : IWarehouseQueryService
{
    public async Task<IEnumerable<Warehouse>> Handle(GetAllWarehousesByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await warehouseRepository.FindAllByBusinessIdAsync(query.BusinessId, cancellationToken);
    }

    public async Task<Warehouse?> Handle(GetWarehouseByIdQuery query, CancellationToken cancellationToken)
    {
        return await warehouseRepository.FindByIdAsync(query.WarehouseId, cancellationToken);
    }
}
