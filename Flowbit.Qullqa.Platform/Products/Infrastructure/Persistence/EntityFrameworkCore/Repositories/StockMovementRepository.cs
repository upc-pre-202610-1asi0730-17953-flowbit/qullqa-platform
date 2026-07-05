using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Products.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Products.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

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
