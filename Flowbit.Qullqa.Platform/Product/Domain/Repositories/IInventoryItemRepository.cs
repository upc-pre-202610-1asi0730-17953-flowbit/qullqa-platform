using Qullqa.Platform.Product.Domain.Model.Aggregates;
using Qullqa.Platform.Shared.Domain.Repositories;

namespace Qullqa.Platform.Product.Domain.Repositories;

public interface IInventoryItemRepository : IBaseRepository<InventoryItem>
{
    Task<IEnumerable<InventoryItem>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
    Task<InventoryItem?> FindByProductAndBusinessAsync(int productId, int businessId, CancellationToken cancellationToken);
}
