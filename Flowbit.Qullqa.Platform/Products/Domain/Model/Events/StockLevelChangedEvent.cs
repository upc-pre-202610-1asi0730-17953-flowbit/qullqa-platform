using Qullqa.Platform.v2.Shared.Domain.Model.Events;

namespace Qullqa.Platform.v2.Products.Domain.Model.Events;

/// <summary>
///     Raised whenever an InventoryItem's stock changes (intake or sale).
///     Alerts & Operational Monitoring subscribes to this to re-evaluate
///     low-stock/out-of-stock alerts reactively — see architecture doc §5.4.
/// </summary>
public record StockLevelChangedEvent(
    int ProductId,
    string ProductName,
    int WarehouseId,
    int BusinessId,
    int NewQuantity,
    int MinimumStock) : IEvent;
