using Qullqa.Platform.Product.Domain.Model.Aggregates;
using Qullqa.Platform.Shared.Domain.Repositories;

namespace Qullqa.Platform.Product.Domain.Repositories;

public interface IWarehouseStockRepository : IBaseRepository<WarehouseStock>
{
    Task<IEnumerable<WarehouseStock>> FindByWarehouseIdAsync(int warehouseId, CancellationToken cancellationToken);
}
