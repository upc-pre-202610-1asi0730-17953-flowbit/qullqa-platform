using Flowbit.Qullqa.Platform.Product.Domain.Model.Enums;
using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Product.Domain.Model.Aggregates;

public class StockMovement : IAuditableEntity
{
    public StockMovement() { }
    public StockMovement(int productId, int businessId, int quantity, MovementType type)
    {
        ProductId = productId; BusinessId = businessId; Quantity = quantity; Type = type;
        RegisteredAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; private set; }
    public int ProductId { get; private set; }
    public int BusinessId { get; private set; }
    public int Quantity { get; private set; }
    public MovementType Type { get; private set; }
    public DateTimeOffset RegisteredAt { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
}
