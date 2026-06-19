using Qullqa.Platform.Alerts.Domain.Model.Enums;

namespace Qullqa.Platform.Alerts.Interfaces.Rest.Resources;

public record CreateAlertResource(int BusinessId, int ProductId, string ProductName, AlertType Type,
    AlertSeverity Severity, string Message, int? BatchId, int? CurrentStock, int? MinStock, int? DaysToExpiry);
