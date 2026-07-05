using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Products.Domain.Repositories;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.v2.Products.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class WarehouseRepository(AppDbContext context) : BaseRepository<Warehouse>(context), IWarehouseRepository
{
    public async Task<IEnumerable<Warehouse>> FindAllByBusinessIdAsync(int businessId,
        CancellationToken cancellationToken = default)
    {
        return await Context.Set<Warehouse>().Where(warehouse => warehouse.BusinessId == businessId)
            .ToListAsync(cancellationToken);
    }
}
