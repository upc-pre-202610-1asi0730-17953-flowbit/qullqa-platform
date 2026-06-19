using Flowbit.Qullqa.Platform.Product.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Product.Domain.Repositories;

public interface IWarehouseStockRepository : IBaseRepository<WarehouseStock>
{
    Task<IEnumerable<WarehouseStock>> FindByWarehouseIdAsync(int warehouseId, CancellationToken cancellationToken);
}
