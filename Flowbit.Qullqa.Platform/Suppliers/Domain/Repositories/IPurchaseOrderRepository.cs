using Qullqa.Platform.Shared.Domain.Repositories;
using Qullqa.Platform.Suppliers.Domain.Model.Aggregates;

namespace Qullqa.Platform.Suppliers.Domain.Repositories;

public interface IPurchaseOrderRepository : IBaseRepository<PurchaseOrder>
{
    Task<IEnumerable<PurchaseOrder>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
    Task<PurchaseOrder?> FindByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
}
