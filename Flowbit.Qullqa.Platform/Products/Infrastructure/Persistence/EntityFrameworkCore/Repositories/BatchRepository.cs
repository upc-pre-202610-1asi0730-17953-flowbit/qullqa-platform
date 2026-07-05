using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Domain.Repositories;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.v2.Products.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class BatchRepository(AppDbContext context) : BaseRepository<Batch>(context), IBatchRepository
{
    public async Task<IEnumerable<Batch>> FindAllByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Batch>().Where(batch => batch.ProductId == productId).ToListAsync(cancellationToken);
    }

    public async Task<Batch?> FindActiveByProductIdAsync(int productId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Batch>()
            .FirstOrDefaultAsync(batch => batch.ProductId == productId && batch.Status == BatchStatus.Active, cancellationToken);
    }

    public async Task<IEnumerable<Batch>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Batch>().Where(batch => batch.BusinessId == businessId).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Batch>> FindAllActiveAsync(CancellationToken cancellationToken = default)
    {
        return await Context.Set<Batch>().Where(batch => batch.Status == BatchStatus.Active).ToListAsync(cancellationToken);
    }
}
