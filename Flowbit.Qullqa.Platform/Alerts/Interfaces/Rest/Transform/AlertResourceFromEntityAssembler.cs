using Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Qullqa.Platform.Alerts.Interfaces.Rest.Resources;

namespace Qullqa.Platform.Alerts.Interfaces.Rest.Transform;

public static class AlertResourceFromEntityAssembler
{
    public static AlertResource ToResourceFromEntity(Alert a) => new(
        a.Id, a.BusinessId, a.ProductId, a.BatchId, a.ProductName, a.Type, a.Severity,
        a.Message, a.Status, a.Date, a.CurrentStock, a.MinStock, a.DaysToExpiry,
        a.Notified, a.NotifiedAt, a.ResolvedAt);
}
