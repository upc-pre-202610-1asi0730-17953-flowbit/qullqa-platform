using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Delivery.Domain.Model.Aggregates;

public class Waypoint : IAuditableEntity
{
    public int Id { get; private set; }
    public int DeliveryId { get; private set; }
    public string Label { get; private set; } = string.Empty;
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public int SequenceOrder { get; private set; }
    public bool Reached { get; private set; }
    public DateTimeOffset? ReachedAt { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    protected Waypoint() { }

    public Waypoint(int deliveryId, string label, double latitude, double longitude, int sequenceOrder)
    {
        DeliveryId = deliveryId;
        Label = label;
        Latitude = latitude;
        Longitude = longitude;
        SequenceOrder = sequenceOrder;
    }

    public void MarkReached()
    {
        Reached = true;
        ReachedAt = DateTimeOffset.UtcNow;
    }
}
