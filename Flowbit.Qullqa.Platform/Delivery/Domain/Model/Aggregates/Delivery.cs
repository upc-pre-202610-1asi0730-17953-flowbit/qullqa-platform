using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Enums;
using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Delivery.Domain.Model.Aggregates;

public class Delivery : IAuditableEntity
{
    public int Id { get; private set; }
    public string TrackingNumber { get; private set; } = string.Empty;
    public int? PurchaseDetailId { get; private set; }
    public int BusinessId { get; private set; }
    public string SupplierName { get; private set; } = string.Empty;
    public string Origin { get; private set; } = string.Empty;
    public string Destination { get; private set; } = string.Empty;
    public string DriverName { get; private set; } = string.Empty;
    public string DriverPhone { get; private set; } = string.Empty;
    public string Vehicle { get; private set; } = string.Empty;
    public string LicensePlate { get; private set; } = string.Empty;
    public DeliveryStatus Status { get; private set; } = DeliveryStatus.Registered;
    public DateTimeOffset RegisteredAt { get; private set; }
    public DateTimeOffset? EstimatedArrival { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public string CurrentLabel { get; private set; } = string.Empty;
    public double? CurrentLatitude { get; private set; }
    public double? CurrentLongitude { get; private set; }
    public decimal TotalWeight { get; private set; }
    public ICollection<Waypoint> Waypoints { get; private set; } = new List<Waypoint>();
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    protected Delivery() { }

    public Delivery(int businessId, string supplierName, string origin, string destination,
        string driverName, string driverPhone, string vehicle, string licensePlate,
        DateTimeOffset? estimatedArrival, decimal totalWeight, int? purchaseDetailId)
    {
        BusinessId = businessId;
        SupplierName = supplierName;
        Origin = origin;
        Destination = destination;
        DriverName = driverName;
        DriverPhone = driverPhone;
        Vehicle = vehicle;
        LicensePlate = licensePlate;
        EstimatedArrival = estimatedArrival;
        TotalWeight = totalWeight;
        PurchaseDetailId = purchaseDetailId;
        RegisteredAt = DateTimeOffset.UtcNow;
        TrackingNumber = GenerateTrackingNumber();
    }

    private static string GenerateTrackingNumber()
        => $"QLL-{DateTimeOffset.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}";

    public void UpdateLocation(string label, double latitude, double longitude)
    {
        CurrentLabel = label;
        CurrentLatitude = latitude;
        CurrentLongitude = longitude;
    }

    public void StartTransit() => Status = DeliveryStatus.InTransit;
    public void ArriveAtDestination() => Status = DeliveryStatus.AtDestination;

    public void Complete()
    {
        Status = DeliveryStatus.Completed;
        CompletedAt = DateTimeOffset.UtcNow;
    }

    public void Cancel() => Status = DeliveryStatus.Cancelled;
}
