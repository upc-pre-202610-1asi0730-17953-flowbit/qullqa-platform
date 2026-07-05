using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class AlertRuleRepository(AppDbContext context) : BaseRepository<AlertRule>(context), IAlertRuleRepository
{
    public async Task<IEnumerable<AlertRule>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<AlertRule>().Where(rule => rule.BusinessId == businessId).ToListAsync(cancellationToken);
    }

    public async Task<AlertRule?> FindByBusinessIdAndTypeAsync(int businessId, string alertType,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<AlertRule>()
            .FirstOrDefaultAsync(rule => rule.BusinessId == businessId && rule.AlertType == alertType, cancellationToken);
    }
}
