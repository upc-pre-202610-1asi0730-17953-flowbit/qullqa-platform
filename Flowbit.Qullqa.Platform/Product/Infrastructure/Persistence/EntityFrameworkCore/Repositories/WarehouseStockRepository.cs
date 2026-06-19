using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Product.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Product.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Product.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class WarehouseStockRepository(AppDbContext context) : BaseRepository<WarehouseStock>(context), IWarehouseStockRepository
{
    public async Task<IEnumerable<WarehouseStock>> FindByWarehouseIdAsync(int warehouseId, CancellationToken cancellationToken)
        => await Context.Set<WarehouseStock>().Where(w => w.WarehouseId == warehouseId).ToListAsync(cancellationToken);
}
