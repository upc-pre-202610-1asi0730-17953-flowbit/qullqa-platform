using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Products.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Products.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

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
