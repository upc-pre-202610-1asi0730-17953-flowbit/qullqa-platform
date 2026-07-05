using Qullqa.Platform.v2.Shared.Domain.Model.Events;

namespace Qullqa.Platform.v2.Products.Domain.Model.Events;

/// <summary>
///     Raised whenever a batch is created or updated (expiration date set).
///     Alerts & Operational Monitoring subscribes to this to re-evaluate
///     expiration alerts reactively — see architecture doc §5.4.
/// </summary>
public record BatchRegisteredEvent(int BatchId, int ProductId, string ProductName, int BusinessId, DateOnly? Expiration) : IEvent;
