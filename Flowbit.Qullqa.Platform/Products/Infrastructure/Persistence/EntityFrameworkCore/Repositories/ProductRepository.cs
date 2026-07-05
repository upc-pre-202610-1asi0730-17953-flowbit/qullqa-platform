using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Products.Domain.Repositories;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.v2.Products.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> FindAllByBusinessIdAsync(int businessId, string? category,
        CancellationToken cancellationToken = default)
    {
        var query = Context.Set<Product>().Where(product => product.BusinessId == businessId);
        if (!string.IsNullOrEmpty(category)) query = query.Where(product => product.Category == category);
        return await query.ToListAsync(cancellationToken);
    }
}
