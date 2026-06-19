using Qullqa.Platform.Product.Domain.Model.Aggregates;
using Qullqa.Platform.Product.Interfaces.Rest.Resources;

namespace Qullqa.Platform.Product.Interfaces.Rest.Transform;

public static class StockMovementResourceFromEntityAssembler
{
    public static StockMovementResource ToResourceFromEntity(StockMovement m)
        => new(m.Id, m.ProductId, m.BusinessId, m.Quantity, m.Type, m.RegisteredAt);
}
