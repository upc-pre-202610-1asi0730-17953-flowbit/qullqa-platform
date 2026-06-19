using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.Sales.Domain.Repositories;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SaleDetailRepository(AppDbContext context) : BaseRepository<SaleDetail>(context), ISaleDetailRepository
{
    public async Task<IEnumerable<SaleDetail>> FindBySaleIdAsync(int saleId, CancellationToken cancellationToken)
        => await Context.Set<SaleDetail>().Where(d => d.SaleId == saleId).ToListAsync(cancellationToken);
}
