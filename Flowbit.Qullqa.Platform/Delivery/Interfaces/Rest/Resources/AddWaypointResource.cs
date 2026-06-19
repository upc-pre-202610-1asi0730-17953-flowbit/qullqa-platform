namespace Flowbit.Qullqa.Platform.Delivery.Interfaces.Rest.Resources;

public record AddWaypointResource(string Label, double Latitude, double Longitude, int SequenceOrder);
