using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Product.Domain.Model.Aggregates;
using Qullqa.Platform.Product.Domain.Repositories;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.Product.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class InventoryItemRepository(AppDbContext context) : BaseRepository<InventoryItem>(context), IInventoryItemRepository
{
    public async Task<IEnumerable<InventoryItem>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<InventoryItem>().Where(i => i.BusinessId == businessId).ToListAsync(cancellationToken);

    public async Task<InventoryItem?> FindByProductAndBusinessAsync(int productId, int businessId, CancellationToken cancellationToken)
        => await Context.Set<InventoryItem>().FirstOrDefaultAsync(i => i.ProductId == productId && i.BusinessId == businessId, cancellationToken);
}
