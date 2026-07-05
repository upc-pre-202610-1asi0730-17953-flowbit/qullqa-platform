using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AlertRepository(AppDbContext context) : BaseRepository<Alert>(context), IAlertRepository
{
    public async Task<IEnumerable<Alert>> FindActiveByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>()
            .Where(alert => alert.BusinessId == businessId && alert.Status != AlertStatus.Resolved)
            .OrderByDescending(alert => alert.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Alert>> FindResolvedByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>()
            .Where(alert => alert.BusinessId == businessId && alert.Status == AlertStatus.Resolved)
            .OrderByDescending(alert => alert.ResolvedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<Alert?> FindActiveByProductAndTypeAsync(int productId, string type, int? batchId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Alert>().FirstOrDefaultAsync(
            alert => alert.ProductId == productId && alert.Type == type && alert.BatchId == batchId
                     && alert.Status != AlertStatus.Resolved,
            cancellationToken);
    }
}
