using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Shared.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Domain.Repositories;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<IEnumerable<Product>> FindAllByBusinessIdAsync(int businessId, string? category,
        CancellationToken cancellationToken = default);
}
