using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.ValueObjects;
using Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Transform;

public static class RegisterWaypointCommandFromResourceAssembler
{
    public static RegisterWaypointCommand ToCommandFromResource(RegisterWaypointResource resource)
    {
        var location = new GeoCoordinate(resource.Location.Latitude, resource.Location.Longitude);
        return new RegisterWaypointCommand(resource.DeliveryId, resource.Label, resource.District, location,
            resource.SequenceOrder);
    }
}
