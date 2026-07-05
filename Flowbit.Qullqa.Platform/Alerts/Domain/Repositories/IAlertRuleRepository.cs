using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;

public interface IAlertRuleRepository : IBaseRepository<AlertRule>
{
    Task<IEnumerable<AlertRule>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);
    Task<AlertRule?> FindByBusinessIdAndTypeAsync(int businessId, string alertType, CancellationToken cancellationToken = default);
}
