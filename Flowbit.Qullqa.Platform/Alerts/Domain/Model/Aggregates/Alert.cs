using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Enums;
using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Alerts.Domain.Model.Aggregates;

public class Alert : IAuditableEntity
{
    public int Id { get; private set; }
    public int BusinessId { get; private set; }
    public int ProductId { get; private set; }
    public int? BatchId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public AlertType Type { get; private set; }
    public AlertSeverity Severity { get; private set; }
    public string Message { get; private set; } = string.Empty;
    public AlertStatus Status { get; private set; } = AlertStatus.Active;
    public DateTimeOffset Date { get; private set; }
    public int? CurrentStock { get; private set; }
    public int? MinStock { get; private set; }
    public int? DaysToExpiry { get; private set; }
    public bool Notified { get; private set; }
    public DateTimeOffset? NotifiedAt { get; private set; }
    public DateTimeOffset? ResolvedAt { get; private set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }

    protected Alert() { }

    public Alert(int businessId, int productId, string productName, AlertType type, AlertSeverity severity,
        string message, int? batchId = null, int? currentStock = null, int? minStock = null, int? daysToExpiry = null)
    {
        BusinessId = businessId;
        ProductId = productId;
        ProductName = productName;
        Type = type;
        Severity = severity;
        Message = message;
        BatchId = batchId;
        CurrentStock = currentStock;
        MinStock = minStock;
        DaysToExpiry = daysToExpiry;
        Date = DateTimeOffset.UtcNow;
    }

    public void Acknowledge() => Status = AlertStatus.Acknowledged;

    public void MarkNotified()
    {
        Notified = true;
        NotifiedAt = DateTimeOffset.UtcNow;
        Status = AlertStatus.Sent;
    }

    public void Resolve()
    {
        Status = AlertStatus.Resolved;
        ResolvedAt = DateTimeOffset.UtcNow;
    }
}
