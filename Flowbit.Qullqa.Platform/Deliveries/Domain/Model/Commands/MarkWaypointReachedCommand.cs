namespace Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Commands;

public record MarkWaypointReachedCommand(int DeliveryId, int WaypointId);
