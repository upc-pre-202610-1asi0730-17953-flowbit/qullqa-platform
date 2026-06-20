using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Enums;
using Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AlertRepository(AppDbContext context) : BaseRepository<Alert>(context), IAlertRepository
{
    public async Task<IEnumerable<Alert>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<Alert>().Where(a => a.BusinessId == businessId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<Alert>> FindActiveByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<Alert>().Where(a => a.BusinessId == businessId && a.Status == AlertStatus.Active).ToListAsync(cancellationToken);
}
