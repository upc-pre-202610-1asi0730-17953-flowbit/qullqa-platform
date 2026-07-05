using Cortex.Mediator.Notifications;

namespace Qullqa.Platform.v2.Shared.Domain.Model.Events;

/// <summary>
///     Marker interface for domain events raised by aggregates across bounded
///     contexts. Extends Cortex.Mediator's own INotification so events are
///     actually dispatchable via IMediator.PublishAsync — not just a
///     documentation-only marker.
/// </summary>
public interface IEvent : INotification
{
}
