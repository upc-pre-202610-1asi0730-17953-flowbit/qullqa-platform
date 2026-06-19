using Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Qullqa.Platform.Alerts.Domain.Model.Queries;

namespace Qullqa.Platform.Alerts.Application.QueryServices;

public interface IAlertQueryService
{
    Task<Alert?> Handle(GetAlertByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Alert>> Handle(GetAlertsByBusinessQuery query, CancellationToken cancellationToken);
}
