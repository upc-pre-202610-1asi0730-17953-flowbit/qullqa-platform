using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Shared.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Domain.Repositories;

public interface IInventoryItemRepository : IBaseRepository<InventoryItem>
{
    Task<InventoryItem?> FindByProductAndWarehouseAsync(int productId, int warehouseId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<InventoryItem>> FindAllByProductIdAsync(int productId, CancellationToken cancellationToken = default);

    Task<IEnumerable<InventoryItem>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);
}
