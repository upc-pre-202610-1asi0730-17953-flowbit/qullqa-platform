using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.ValueObjects;

namespace Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Commands;

public record UpdateDeliveryStatusCommand(int DeliveryId, string Status, string? CurrentLabel, GeoCoordinate? CurrentLocation);
