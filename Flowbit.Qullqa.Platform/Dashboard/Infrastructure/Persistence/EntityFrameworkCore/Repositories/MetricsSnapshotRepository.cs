using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MetricsSnapshotRepository(AppDbContext context) : BaseRepository<MetricsSnapshot>(context), IMetricsSnapshotRepository
{
    public async Task<IEnumerable<MetricsSnapshot>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<MetricsSnapshot>().Where(m => m.BusinessId == businessId).ToListAsync(cancellationToken);

    public async Task<MetricsSnapshot?> FindLatestByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<MetricsSnapshot>().Where(m => m.BusinessId == businessId).OrderByDescending(m => m.Date).FirstOrDefaultAsync(cancellationToken);
}
