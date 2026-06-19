using Qullqa.Platform.Alerts.Domain.Model.Enums;

namespace Qullqa.Platform.Alerts.Domain.Model.Commands;

public record UpdateAlertStatusCommand(int Id, AlertStatus Status);
