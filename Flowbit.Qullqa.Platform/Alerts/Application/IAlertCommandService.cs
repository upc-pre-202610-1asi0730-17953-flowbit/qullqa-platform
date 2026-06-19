using Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Qullqa.Platform.Alerts.Domain.Model.Commands;
using Qullqa.Platform.Shared.Application.Model;

namespace Qullqa.Platform.Alerts.Application.CommandServices;

public interface IAlertCommandService
{
    Task<Result<Alert>> Handle(CreateAlertCommand command, CancellationToken cancellationToken);
    Task<Result<Alert>> Handle(UpdateAlertStatusCommand command, CancellationToken cancellationToken);
}
