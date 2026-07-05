using Qullqa.Platform.v2.Shared.Domain.Repositories;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Aggregates;

namespace Qullqa.Platform.v2.Suppliers.Domain.Repositories;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task<IEnumerable<Supplier>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);
}
