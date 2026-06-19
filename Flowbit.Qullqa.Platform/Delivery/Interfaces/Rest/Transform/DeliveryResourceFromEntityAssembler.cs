using Flowbit.Qullqa.Platform.Delivery.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Delivery.Interfaces.Rest.Transform;

public static class DeliveryResourceFromEntityAssembler
{
    public static DeliveryResource ToResourceFromEntity(Domain.Model.Aggregates.Delivery d) => new(
        d.Id, d.TrackingNumber, null, d.BusinessId, d.SupplierName, d.Origin, d.Destination,
        d.DriverName, d.DriverPhone, d.Vehicle, d.LicensePlate, d.Status,
        d.RegisteredAt, d.EstimatedArrival, d.CompletedAt,
        d.CurrentLabel, d.CurrentLatitude, d.CurrentLongitude, d.TotalWeight, d.PurchaseDetailId,
        Array.Empty<string>(),
        d.Waypoints.Select(w => new WaypointResource(w.Id, w.DeliveryId, w.Label, string.Empty, w.Latitude, w.Longitude, w.SequenceOrder, w.Reached, w.ReachedAt)));
}
