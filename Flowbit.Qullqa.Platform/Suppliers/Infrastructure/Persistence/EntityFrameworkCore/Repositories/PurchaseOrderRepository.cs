using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class PurchaseOrderRepository(AppDbContext context) : BaseRepository<PurchaseOrder>(context), IPurchaseOrderRepository
{
    public async Task<IEnumerable<PurchaseOrder>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<PurchaseOrder>().Where(o => o.BusinessId == businessId).Include(o => o.Details).ToListAsync(cancellationToken);

    public async Task<PurchaseOrder?> FindByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
        => await Context.Set<PurchaseOrder>().Include(o => o.Details).FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
}
