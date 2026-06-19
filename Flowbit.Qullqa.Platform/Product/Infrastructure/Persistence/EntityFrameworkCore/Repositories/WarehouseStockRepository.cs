using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Product.Domain.Model.Aggregates;
using Qullqa.Platform.Product.Domain.Repositories;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.Product.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class WarehouseStockRepository(AppDbContext context) : BaseRepository<WarehouseStock>(context), IWarehouseStockRepository
{
    public async Task<IEnumerable<WarehouseStock>> FindByWarehouseIdAsync(int warehouseId, CancellationToken cancellationToken)
        => await Context.Set<WarehouseStock>().Where(w => w.WarehouseId == warehouseId).ToListAsync(cancellationToken);
}
