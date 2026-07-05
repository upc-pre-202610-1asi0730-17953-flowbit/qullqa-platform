using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;

public static class StockMovementResourceFromEntityAssembler
{
    public static StockMovementResource ToResourceFromEntity(StockMovement movement)
    {
        return new StockMovementResource(movement.Id, movement.ProductId, movement.BusinessId, movement.WarehouseId,
            movement.Quantity, movement.Type, movement.Supplier, movement.Note, movement.RegisteredAt);
    }
}
