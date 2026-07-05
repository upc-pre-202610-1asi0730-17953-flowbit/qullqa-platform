using Flowbit.Qullqa.Platform.Shared.Domain.Model.Events;

namespace Flowbit.Qullqa.Platform.Products.Domain.Model.Events;

public record ProductCreatedEvent(int ProductId, int BusinessId, string Name) : IEvent;
