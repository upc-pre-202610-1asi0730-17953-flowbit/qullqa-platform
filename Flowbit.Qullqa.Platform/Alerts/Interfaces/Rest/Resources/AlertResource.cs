using Qullqa.Platform.Alerts.Domain.Model.Enums;

namespace Qullqa.Platform.Alerts.Interfaces.Rest.Resources;

public record AlertResource(int Id, int BusinessId, int ProductId, int? BatchId, string ProductName,
    AlertType Type, AlertSeverity Severity, string Message, AlertStatus Status, DateTimeOffset Date,
    int? CurrentStock, int? MinStock, int? DaysToExpiry, bool Notified, DateTimeOffset? NotifiedAt, DateTimeOffset? ResolvedAt);
