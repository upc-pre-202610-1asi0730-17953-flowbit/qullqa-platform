using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;

public static class InventoryItemResourceFromEntityAssembler
{
    public static InventoryItemResource ToResourceFromEntity(InventoryItem item)
    {
        return new InventoryItemResource(item.Id, item.ProductId, item.WarehouseId, item.BusinessId, item.StockUnit,
            item.MinimumStock, item.UpdatedAt);
    }
}
