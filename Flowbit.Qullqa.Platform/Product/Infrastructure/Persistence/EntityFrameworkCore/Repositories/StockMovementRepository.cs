using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Product.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Product.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Product.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class StockMovementRepository(AppDbContext context) : BaseRepository<StockMovement>(context), IStockMovementRepository
{
    public async Task<IEnumerable<StockMovement>> FindByProductAndBusinessAsync(int productId, int businessId, CancellationToken cancellationToken)
        => await Context.Set<StockMovement>().Where(m => m.ProductId == productId && m.BusinessId == businessId).ToListAsync(cancellationToken);
}
