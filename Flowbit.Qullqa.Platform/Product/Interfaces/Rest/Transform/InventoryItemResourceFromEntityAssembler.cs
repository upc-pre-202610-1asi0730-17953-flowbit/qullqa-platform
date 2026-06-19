using Qullqa.Platform.Product.Domain.Model.Aggregates;
using Qullqa.Platform.Product.Interfaces.Rest.Resources;

namespace Qullqa.Platform.Product.Interfaces.Rest.Transform;

public static class InventoryItemResourceFromEntityAssembler
{
    public static InventoryItemResource ToResourceFromEntity(InventoryItem i)
        => new(i.Id, i.ProductId, i.BusinessId, i.WarehouseId, i.CurrentStock, i.MinimumStock);
}
