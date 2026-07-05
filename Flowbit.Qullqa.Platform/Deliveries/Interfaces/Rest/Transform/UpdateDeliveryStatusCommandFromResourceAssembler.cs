using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.ValueObjects;
using Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Transform;

public static class UpdateDeliveryStatusCommandFromResourceAssembler
{
    public static UpdateDeliveryStatusCommand ToCommandFromResource(UpdateDeliveryStatusResource resource, int deliveryId)
    {
        var location = resource.CurrentLocation != null
            ? new GeoCoordinate(resource.CurrentLocation.Latitude, resource.CurrentLocation.Longitude)
            : null;
        return new UpdateDeliveryStatusCommand(deliveryId, resource.Status, resource.CurrentLabel, location);
    }
}
