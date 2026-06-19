using Qullqa.Platform.Alerts.Application.QueryServices;
using Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Qullqa.Platform.Alerts.Domain.Model.Queries;
using Qullqa.Platform.Alerts.Domain.Repositories;

namespace Qullqa.Platform.Alerts.Application.Internal.QueryServices;

public class AlertQueryService(IAlertRepository alertRepository) : IAlertQueryService
{
    public async Task<Alert?> Handle(GetAlertByIdQuery query, CancellationToken cancellationToken)
        => await alertRepository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<Alert>> Handle(GetAlertsByBusinessQuery query, CancellationToken cancellationToken)
        => await alertRepository.FindByBusinessIdAsync(query.BusinessId, cancellationToken);
}
