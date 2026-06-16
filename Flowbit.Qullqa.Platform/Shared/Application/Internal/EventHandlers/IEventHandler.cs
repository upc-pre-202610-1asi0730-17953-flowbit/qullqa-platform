using Flowbit.Qullqa.Platform.Shared.Domain.Model.Events;
using Cortex.Mediator.Notifications;


namespace Flowbit.Qullqa.Platform.Shared.Application.Internal.EventHandlers;


public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}