namespace Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Commands;

/// <summary>
///     When PurchaseDetailId is provided, SupplierName is autofilled via
///     ISupplierContextFacade and the resource-supplied value is ignored —
///     see DeliveryCommandService.
/// </summary>
public record CreateDeliveryCommand(
    int BusinessId,
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
