namespace Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Resources;

public record UpdateDeliveryStatusResource(string Status, string? CurrentLabel, GeoCoordinateResource? CurrentLocation);
