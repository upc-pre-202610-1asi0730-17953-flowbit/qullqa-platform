using Qullqa.Platform.Product.Domain.Model.Aggregates;
using Qullqa.Platform.Shared.Domain.Repositories;

namespace Qullqa.Platform.Product.Domain.Repositories;

public interface IStockMovementRepository : IBaseRepository<StockMovement>
{
    Task<IEnumerable<StockMovement>> FindByProductAndBusinessAsync(int productId, int businessId, CancellationToken cancellationToken);
}
