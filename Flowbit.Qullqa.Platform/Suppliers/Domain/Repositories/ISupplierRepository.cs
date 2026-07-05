using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;

namespace Flowbit.Qullqa.Platform.Suppliers.Domain.Repositories;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task<IEnumerable<Supplier>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);
}
