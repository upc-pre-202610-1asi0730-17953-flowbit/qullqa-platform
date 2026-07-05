using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.ValueObjects;

namespace Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Aggregates;

public static class DeliveryStatus
{
    public const string Registered = "REGISTERED";
    public const string InTransit = "IN_TRANSIT";
    public const string AtDestination = "AT_DESTINATION";
    public const string Completed = "COMPLETED";
    public const string Cancelled = "CANCELLED";
}

/// <summary>
///     A shipment being tracked. May be linked to a purchase order line
///     (PurchaseDetailId) or fully independent — both are valid (see
///     architecture doc §6.7). Real-time IoT tracking is a Premium-plan
///     feature (US19) — not gated here since feature-gating by plan is
///     explicitly out of scope for this version (§5.5/§8.2).
/// </summary>
public class Delivery
{
    private readonly List<Waypoint> _waypoints = [];

    public Delivery(
        int businessId,
        string trackingNumber,
        string orderId,
        string supplierName,
        string origin,
        string destination,
        string driverName,
        string driverPhone,
        string vehicle,
        string licensePlate,
        DateTimeOffset estimatedArrival,
        decimal totalWeightValue,
        string totalWeightUnit,
        int? purchaseDetailId)
    {
        BusinessId = businessId;
        TrackingNumber = trackingNumber;
        OrderId = orderId;
        SupplierName = supplierName;
        Origin = origin;
        Destination = destination;
        DriverName = driverName;
        DriverPhone = driverPhone;
        Vehicle = vehicle;
        LicensePlate = licensePlate;
        Status = DeliveryStatus.Registered;
        RegisteredAt = DateTimeOffset.UtcNow;
        EstimatedArrival = estimatedArrival;
        TotalWeightValue = totalWeightValue;
        TotalWeightUnit = totalWeightUnit;
        PurchaseDetailId = purchaseDetailId;
    }

    public Delivery()
    {
        TrackingNumber = string.Empty;
        OrderId = string.Empty;
        SupplierName = string.Empty;
        Origin = string.Empty;
        Destination = string.Empty;
        DriverName = string.Empty;
        DriverPhone = string.Empty;
        Vehicle = string.Empty;
        LicensePlate = string.Empty;
        Status = DeliveryStatus.Registered;
        TotalWeightUnit = string.Empty;
    }

    public int Id { get; }
    public int BusinessId { get; private set; }
    public string TrackingNumber { get; private set; }

    /// <summary>Free-text, not a real FK — a delivery may reference an order from any external/legacy system.</summary>
    public string OrderId { get; private set; }

    public string SupplierName { get; private set; }
    public string Origin { get; private set; }
    public string Destination { get; private set; }
    public string DriverName { get; private set; }
    public string DriverPhone { get; private set; }
    public string Vehicle { get; private set; }
    public string LicensePlate { get; private set; }
    public string Status { get; private set; }
    public DateTimeOffset RegisteredAt { get; private set; }
    public DateTimeOffset EstimatedArrival { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
    public string? CurrentLabel { get; private set; }
    public GeoCoordinate? CurrentLocation { get; private set; }
    public decimal TotalWeightValue { get; private set; }
    public string TotalWeightUnit { get; private set; }

    /// <summary>Real FK, optional — set only when this delivery is linked to a purchase order line (see Suppliers §6.6).</summary>
    public int? PurchaseDetailId { get; private set; }

    public IReadOnlyCollection<Waypoint> Waypoints => _waypoints.AsReadOnly();

    /// <summary>Proportion of waypoints reached so far — 0 when there are none yet.</summary>
    public double RouteProgress => _waypoints.Count == 0 ? 0 : (double)_waypoints.Count(waypoint => waypoint.Reached) / _waypoints.Count;

    public Waypoint AddWaypoint(string label, string district, GeoCoordinate location, int sequenceOrder)
    {
        var waypoint = new Waypoint(Id, label, district, location, sequenceOrder);
        _waypoints.Add(waypoint);
        return waypoint;
    }

    public Delivery UpdateStatus(string status, string? currentLabel, GeoCoordinate? currentLocation)
    {
        Status = status;
        if (currentLabel != null) CurrentLabel = currentLabel;
        if (currentLocation != null) CurrentLocation = currentLocation;
        if (status == DeliveryStatus.Completed) CompletedAt = DateTimeOffset.UtcNow;
        return this;
    }
}
