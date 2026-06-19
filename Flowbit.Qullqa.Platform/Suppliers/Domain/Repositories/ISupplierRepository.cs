using Qullqa.Platform.Shared.Domain.Repositories;
using Qullqa.Platform.Suppliers.Domain.Model.Aggregates;

namespace Qullqa.Platform.Suppliers.Domain.Repositories;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task<IEnumerable<Supplier>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
    Task<bool> ExistsByRucAsync(string ruc, CancellationToken cancellationToken);
}
