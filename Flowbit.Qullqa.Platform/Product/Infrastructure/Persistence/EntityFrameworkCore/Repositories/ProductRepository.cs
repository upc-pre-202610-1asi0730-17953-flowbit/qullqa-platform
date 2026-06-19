using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Product.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Product.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ProductRepository(AppDbContext context) : BaseRepository<Domain.Model.Aggregates.Product>(context), IProductRepository
{
    public async Task<IEnumerable<Domain.Model.Aggregates.Product>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<Domain.Model.Aggregates.Product>().Where(p => p.BusinessId == businessId).ToListAsync(cancellationToken);
}
