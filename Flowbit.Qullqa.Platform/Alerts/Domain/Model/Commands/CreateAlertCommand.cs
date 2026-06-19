using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Alerts.Domain.Model.Commands;

public record CreateAlertCommand(int BusinessId, int ProductId, string ProductName, AlertType Type,
    AlertSeverity Severity, string Message, int? BatchId, int? CurrentStock, int? MinStock, int? DaysToExpiry);
