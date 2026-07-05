using Flowbit.Qullqa.Platform.Alerts.Application.QueryServices;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Alerts.Application.Internal.QueryServices;

public class AlertQueryService(IAlertRepository alertRepository) : IAlertQueryService
{
    public async Task<IEnumerable<Alert>> Handle(GetActiveAlertsByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await alertRepository.FindActiveByBusinessIdAsync(query.BusinessId, cancellationToken);
    }

    public async Task<IEnumerable<Alert>> Handle(GetAlertHistoryByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await alertRepository.FindResolvedByBusinessIdAsync(query.BusinessId, cancellationToken);
    }
}
