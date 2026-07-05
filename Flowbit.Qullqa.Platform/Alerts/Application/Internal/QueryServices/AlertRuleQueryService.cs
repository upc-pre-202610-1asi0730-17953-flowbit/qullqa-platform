using Flowbit.Qullqa.Platform.Alerts.Application.QueryServices;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Alerts.Application.Internal.QueryServices;

public class AlertRuleQueryService(IAlertRuleRepository alertRuleRepository) : IAlertRuleQueryService
{
    public async Task<IEnumerable<AlertRule>> Handle(GetAlertRulesByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await alertRuleRepository.FindAllByBusinessIdAsync(query.BusinessId, cancellationToken);
    }
}
