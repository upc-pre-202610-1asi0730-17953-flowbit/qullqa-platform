using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Sales.Domain.Repositories;

public interface ISaleDetailRepository : IBaseRepository<SaleDetail>
{
    Task<IEnumerable<SaleDetail>> FindBySaleIdAsync(int saleId, CancellationToken cancellationToken);
}
