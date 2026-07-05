using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Alerts.Application.QueryServices;

public interface IAlertRuleQueryService
{
    Task<IEnumerable<AlertRule>> Handle(GetAlertRulesByBusinessIdQuery query, CancellationToken cancellationToken);
}
