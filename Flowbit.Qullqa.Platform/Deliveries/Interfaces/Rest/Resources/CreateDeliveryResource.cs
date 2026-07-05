namespace Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Resources;

public record CreateDeliveryResource(
    string TrackingNumber,
    string OrderId,
    string SupplierName,
    string Origin,
    string Destination,
    string DriverName,
    string DriverPhone,
    string Vehicle,
    string LicensePlate,
    DateTimeOffset EstimatedArrival,
    decimal TotalWeightValue,
    string TotalWeightUnit,
    int? PurchaseDetailId);
