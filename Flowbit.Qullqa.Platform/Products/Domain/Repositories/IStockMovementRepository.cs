using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Shared.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Domain.Repositories;

public interface IStockMovementRepository : IBaseRepository<StockMovement>
{
    Task<IEnumerable<StockMovement>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);
}
