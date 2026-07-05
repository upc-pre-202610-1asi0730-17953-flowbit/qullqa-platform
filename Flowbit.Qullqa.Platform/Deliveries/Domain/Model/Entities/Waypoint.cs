using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.ValueObjects;

namespace Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Entities;

/// <summary>A stop along a delivery's route. Lives inside the Delivery aggregate boundary.</summary>
public class Waypoint(int deliveryId, string label, string district, GeoCoordinate location, int sequenceOrder)
{
    public Waypoint() : this(0, string.Empty, string.Empty, new GeoCoordinate(0, 0), 0)
    {
    }

    public int Id { get; }
    public int DeliveryId { get; private set; } = deliveryId;
    public string Label { get; private set; } = label;
    public string District { get; private set; } = district;
    public GeoCoordinate Location { get; private set; } = location;
    public DateTimeOffset? Timestamp { get; private set; }
    public bool Reached { get; private set; }
    public int SequenceOrder { get; private set; } = sequenceOrder;

    public Waypoint MarkReached()
    {
        Reached = true;
        Timestamp = DateTimeOffset.UtcNow;
        return this;
    }
}
