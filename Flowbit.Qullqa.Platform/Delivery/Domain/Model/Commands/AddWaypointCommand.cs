namespace Flowbit.Qullqa.Platform.Delivery.Domain.Model.Commands;

public record AddWaypointCommand(int DeliveryId, string Label, double Latitude, double Longitude, int SequenceOrder);
