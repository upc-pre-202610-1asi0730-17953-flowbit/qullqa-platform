using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Alerts.Application.CommandServices;

public interface IAlertRuleCommandService
{
    Task<Result<AlertRule>> Handle(CreateOrUpdateAlertRuleCommand command, CancellationToken cancellationToken);
}
