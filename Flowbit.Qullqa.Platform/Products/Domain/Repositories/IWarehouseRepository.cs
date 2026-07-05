using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Shared.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Domain.Repositories;

public interface IWarehouseRepository : IBaseRepository<Warehouse>
{
    Task<IEnumerable<Warehouse>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);
}
