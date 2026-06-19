using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Alerts.Domain.Model.Commands;

public record UpdateAlertStatusCommand(int Id, AlertStatus Status);
