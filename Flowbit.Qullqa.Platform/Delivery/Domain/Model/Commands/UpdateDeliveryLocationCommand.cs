namespace Flowbit.Qullqa.Platform.Delivery.Domain.Model.Commands;

public record UpdateDeliveryLocationCommand(int Id, string Label, double Latitude, double Longitude);
