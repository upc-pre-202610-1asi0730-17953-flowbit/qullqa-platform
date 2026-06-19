using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Product.Domain.Model.Aggregates;

public class InventoryItem : IAuditableEntity
{
    public InventoryItem() { }
    public InventoryItem(int productId, int businessId, int warehouseId, int currentStock, int minimumStock)
    {
        ProductId = productId; BusinessId = businessId; WarehouseId = warehouseId;
        CurrentStock = currentStock; MinimumStock = minimumStock;
    }

    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public int BusinessId { get; private set; }
    public int WarehouseId { get; private set; }
    public int CurrentStock { get; private set; }
    public int MinimumStock { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public InventoryItem UpdateStock(int quantity) { CurrentStock += quantity; return this; }
    public InventoryItem SetMinimumStock(int minimumStock) { MinimumStock = minimumStock; return this; }
}
