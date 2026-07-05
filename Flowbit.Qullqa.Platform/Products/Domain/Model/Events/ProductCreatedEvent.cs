using Qullqa.Platform.v2.Shared.Domain.Model.Events;

namespace Qullqa.Platform.v2.Products.Domain.Model.Events;

public record ProductCreatedEvent(int ProductId, int BusinessId, string Name) : IEvent;
