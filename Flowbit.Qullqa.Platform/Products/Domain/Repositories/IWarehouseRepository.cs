using Flowbit.Qullqa.Platform.Products.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Products.Domain.Repositories;

public interface IWarehouseRepository : IBaseRepository<Warehouse>
{
    Task<IEnumerable<Warehouse>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);
}
