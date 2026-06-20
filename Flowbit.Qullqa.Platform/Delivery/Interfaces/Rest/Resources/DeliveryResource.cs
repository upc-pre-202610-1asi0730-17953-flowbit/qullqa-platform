using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Delivery.Interfaces.Rest.Resources;

public record DeliveryResource(int Id, string TrackingNumber, string? OrderId, int BusinessId, string SupplierName,
    string Origin, string Destination, string DriverName, string DriverPhone, string Vehicle, string LicensePlate,
    DeliveryStatus Status, DateTimeOffset RegisteredAt, DateTimeOffset? EstimatedArrival, DateTimeOffset? CompletedAt,
    string CurrentLabel, double? CurrentLatitude, double? CurrentLongitude, decimal TotalWeight, int? PurchaseDetailId,
    IEnumerable<string> Products, IEnumerable<WaypointResource> Waypoints);
