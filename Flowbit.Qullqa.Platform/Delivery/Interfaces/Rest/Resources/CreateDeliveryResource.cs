namespace Flowbit.Qullqa.Platform.Delivery.Interfaces.Rest.Resources;

public record CreateDeliveryResource(int BusinessId, string SupplierName, string Origin, string Destination,
    string DriverName, string DriverPhone, string Vehicle, string LicensePlate,
    DateTimeOffset? EstimatedArrival, decimal TotalWeight, int? PurchaseDetailId);
