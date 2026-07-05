using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Alerts.Application.CommandServices;

public interface IAlertCommandService
{
    Task<Result<Alert>> Handle(CreateAlertCommand command, CancellationToken cancellationToken);
    Task<Result<Alert>> Handle(AcknowledgeAlertCommand command, CancellationToken cancellationToken);
    Task<Result<Alert>> Handle(ResolveAlertCommand command, CancellationToken cancellationToken);
}
