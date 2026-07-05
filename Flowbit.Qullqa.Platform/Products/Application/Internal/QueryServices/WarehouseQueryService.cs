using Flowbit.Qullqa.Platform.Products.Application.QueryServices;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Products.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Products.Application.Internal.QueryServices;

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
