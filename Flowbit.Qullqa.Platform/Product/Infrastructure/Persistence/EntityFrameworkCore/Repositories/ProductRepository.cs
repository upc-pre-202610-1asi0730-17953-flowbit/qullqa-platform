using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Product.Domain.Repositories;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.Product.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ProductRepository(AppDbContext context) : BaseRepository<Domain.Model.Aggregates.Product>(context), IProductRepository
{
    public async Task<IEnumerable<Domain.Model.Aggregates.Product>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<Domain.Model.Aggregates.Product>().Where(p => p.BusinessId == businessId).ToListAsync(cancellationToken);
}
