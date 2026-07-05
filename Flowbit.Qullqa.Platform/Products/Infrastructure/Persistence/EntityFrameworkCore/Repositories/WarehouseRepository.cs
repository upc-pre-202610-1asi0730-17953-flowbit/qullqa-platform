using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Products.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Products.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class WarehouseRepository(AppDbContext context) : BaseRepository<Warehouse>(context), IWarehouseRepository
{
    public async Task<IEnumerable<Warehouse>> FindAllByBusinessIdAsync(int businessId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Warehouse>().Where(warehouse => warehouse.BusinessId == businessId)
            .ToListAsync(cancellationToken);
    }
}
