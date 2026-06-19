using Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.Shared.Domain.Repositories;

namespace Qullqa.Platform.Sales.Domain.Repositories;

public interface ISaleDetailRepository : IBaseRepository<SaleDetail>
{
    Task<IEnumerable<SaleDetail>> FindBySaleIdAsync(int saleId, CancellationToken cancellationToken);
}
