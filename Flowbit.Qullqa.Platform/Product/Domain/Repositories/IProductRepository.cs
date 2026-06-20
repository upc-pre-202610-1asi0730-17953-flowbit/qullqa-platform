using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Product.Domain.Repositories;

public interface IProductRepository : IBaseRepository<Model.Aggregates.Product>
{
    Task<IEnumerable<Model.Aggregates.Product>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
}
