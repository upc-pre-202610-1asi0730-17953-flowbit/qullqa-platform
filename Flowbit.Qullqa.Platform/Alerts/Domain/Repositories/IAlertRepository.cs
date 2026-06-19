using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;

public interface IAlertRepository : IBaseRepository<Alert>
{
    Task<IEnumerable<Alert>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
    Task<IEnumerable<Alert>> FindActiveByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
}
