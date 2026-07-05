using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Domain.Repositories;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.v2.Products.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class StockMovementRepository(AppDbContext context) : BaseRepository<StockMovement>(context), IStockMovementRepository
{
    public async Task<IEnumerable<StockMovement>> FindAllByBusinessIdAsync(int businessId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<StockMovement>().Where(movement => movement.BusinessId == businessId)
            .OrderByDescending(movement => movement.RegisteredAt)
            .ToListAsync(cancellationToken);
    }
}
