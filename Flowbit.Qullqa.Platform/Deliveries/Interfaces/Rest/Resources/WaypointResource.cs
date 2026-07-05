namespace Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Resources;

public record WaypointResource(
    int Id,
    int DeliveryId,
    string Label,
    string District,
    GeoCoordinateResource Location,
    DateTimeOffset? Timestamp,
    bool Reached,
    int SequenceOrder);
