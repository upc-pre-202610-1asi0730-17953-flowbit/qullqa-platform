namespace Flowbit.Qullqa.Platform.Delivery.Interfaces.Rest.Resources;

public record WaypointResource(int Id, int DeliveryId, string Label, string District, double Latitude, double Longitude,
    int SequenceOrder, bool Reached, DateTimeOffset? Timestamp);
