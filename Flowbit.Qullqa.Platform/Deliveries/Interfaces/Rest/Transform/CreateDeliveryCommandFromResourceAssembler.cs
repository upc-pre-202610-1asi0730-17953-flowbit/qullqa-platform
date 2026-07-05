using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Transform;

public static class CreateDeliveryCommandFromResourceAssembler
{
    public static CreateDeliveryCommand ToCommandFromResource(CreateDeliveryResource resource, int businessId)
    {
        return new CreateDeliveryCommand(businessId, resource.TrackingNumber, resource.OrderId, resource.SupplierName,
            resource.Origin, resource.Destination, resource.DriverName, resource.DriverPhone, resource.Vehicle,
            resource.LicensePlate, resource.EstimatedArrival, resource.TotalWeightValue, resource.TotalWeightUnit,
            resource.PurchaseDetailId);
    }
}
