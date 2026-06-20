namespace Flowbit.Qullqa.Platform.Delivery.Domain.Model.Commands;

public record CreateDeliveryCommand(int BusinessId, string SupplierName, string Origin, string Destination,
    string DriverName, string DriverPhone, string Vehicle, string LicensePlate,
    DateTimeOffset? EstimatedArrival, decimal TotalWeight, int? PurchaseDetailId);
