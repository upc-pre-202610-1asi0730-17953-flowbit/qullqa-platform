using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Alerts.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Alerts.Interfaces.Rest.Transform;

public static class CreateAlertCommandFromResourceAssembler
{
    public static CreateAlertCommand ToCommandFromResource(CreateAlertResource resource, int businessId)
    {
        return new CreateAlertCommand(businessId, resource.ProductId, resource.BatchId, resource.ProductName, resource.Type,
            resource.Severity, resource.Message, resource.CurrentStock, resource.MinStock, resource.DaysToExpiry);
    }
}
