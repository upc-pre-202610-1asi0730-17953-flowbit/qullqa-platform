using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Product.Domain.Model.Aggregates;

public class WarehouseStock : IAuditableEntity
{
    public WarehouseStock() { }
    public WarehouseStock(int warehouseId, int productId, int stock)
    {
        WarehouseId = warehouseId; ProductId = productId; Stock = stock;
    }

    public int Id { get; private set; }
    public int WarehouseId { get; private set; }
    public int ProductId { get; private set; }
    public int Stock { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public WarehouseStock UpdateStock(int stock) { Stock = stock; return this; }
}
