namespace Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Resources;

public record RegisterWaypointResource(
    int DeliveryId,
    string Label,
    string District,
    GeoCoordinateResource Location,
    int SequenceOrder);
