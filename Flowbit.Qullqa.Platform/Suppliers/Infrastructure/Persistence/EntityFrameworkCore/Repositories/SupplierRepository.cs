using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SupplierRepository(AppDbContext context) : BaseRepository<Supplier>(context), ISupplierRepository
{
    public async Task<IEnumerable<Supplier>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Supplier>().Where(supplier => supplier.BusinessId == businessId).ToListAsync(cancellationToken);
    }
}
