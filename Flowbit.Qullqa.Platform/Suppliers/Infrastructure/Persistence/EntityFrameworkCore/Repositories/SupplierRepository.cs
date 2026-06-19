using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.Suppliers.Domain.Repositories;

namespace Qullqa.Platform.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SupplierRepository(AppDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
{
    public async Task<IEnumerable<Supplier>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<Supplier>().Where(s => s.BusinessId == businessId).ToListAsync(cancellationToken);

    public async Task<bool> ExistsByRucAsync(string ruc, CancellationToken cancellationToken)
        => await Context.Set<Supplier>().AnyAsync(s => s.Ruc == ruc, cancellationToken);
}
