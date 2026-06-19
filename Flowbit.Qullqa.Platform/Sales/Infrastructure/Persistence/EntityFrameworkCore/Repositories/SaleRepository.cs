using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.Sales.Domain.Repositories;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SaleRepository(AppDbContext context) : BaseRepository<Sale>(context), ISaleRepository
{
    public async Task<IEnumerable<Sale>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<Sale>().Where(s => s.BusinessId == businessId).Include(s => s.Details).ToListAsync(cancellationToken);

    public async Task<Sale?> FindByIdWithDetailsAsync(int id, CancellationToken cancellationToken)
        => await Context.Set<Sale>().Include(s => s.Details).FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
}
