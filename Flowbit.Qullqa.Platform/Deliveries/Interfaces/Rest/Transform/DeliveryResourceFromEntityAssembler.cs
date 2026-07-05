using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Transform;

public static class DeliveryResourceFromEntityAssembler
{
    public static DeliveryResource ToResourceFromEntity(Delivery delivery)
    {
        var currentLocation = delivery.CurrentLocation != null
            ? new GeoCoordinateResource(delivery.CurrentLocation.Latitude, delivery.CurrentLocation.Longitude)
            : null;
        var waypoints = delivery.Waypoints.Select(ToResourceFromEntity).ToList();

        return new DeliveryResource(delivery.Id, delivery.BusinessId, delivery.TrackingNumber, delivery.OrderId,
            delivery.SupplierName, delivery.Origin, delivery.Destination, delivery.DriverName, delivery.DriverPhone,
            delivery.Vehicle, delivery.LicensePlate, delivery.Status, delivery.RegisteredAt, delivery.EstimatedArrival,
            delivery.CompletedAt, delivery.CurrentLabel, currentLocation, delivery.TotalWeightValue,
            delivery.TotalWeightUnit, delivery.PurchaseDetailId, delivery.RouteProgress, waypoints);
    }

    public static WaypointResource ToResourceFromEntity(Waypoint waypoint)
    {
        var location = new GeoCoordinateResource(waypoint.Location.Latitude, waypoint.Location.Longitude);
        return new WaypointResource(waypoint.Id, waypoint.DeliveryId, waypoint.Label, waypoint.District, location,
            waypoint.Timestamp, waypoint.Reached, waypoint.SequenceOrder);
    }
}
