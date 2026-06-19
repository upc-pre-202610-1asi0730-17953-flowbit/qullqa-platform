using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SaleRepository(AppDbContext context) : BaseRepository<Sale>(context), ISaleRepository
{
    public async Task<IEnumerable<Sale>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<Sale>().Where(s => s.BusinessId == businessId).Include(s => s.Details).ToListAsync(cancellationToken);

    public async Task<Sale?> FindByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
        => await Context.Set<Sale>().Include(s => s.Details).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
}
