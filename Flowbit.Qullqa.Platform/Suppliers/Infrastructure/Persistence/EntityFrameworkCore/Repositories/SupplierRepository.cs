using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Suppliers.Domain.Repositories;

namespace Qullqa.Platform.v2.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SupplierRepository(AppDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
{
    public async Task<IEnumerable<Supplier>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Supplier>().Where(supplier => supplier.BusinessId == businessId).ToListAsync(cancellationToken);
    }
}
