using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SaleDetailRepository(AppDbContext context) : BaseRepository<SaleDetail>(context), ISaleDetailRepository
{
    public async Task<IEnumerable<SaleDetail>> FindBySaleIdAsync(int saleId, CancellationToken cancellationToken)
        => await Context.Set<SaleDetail>().Where(d => d.SaleId == saleId).ToListAsync(cancellationToken);
}
