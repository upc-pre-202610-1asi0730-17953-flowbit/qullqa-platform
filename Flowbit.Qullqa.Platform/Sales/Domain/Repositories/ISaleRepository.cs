using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Sales.Domain.Repositories;

public interface ISaleRepository : IBaseRepository<Sale>
{
    Task<IEnumerable<Sale>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
    Task<Sale?> FindByIdWithDetailsAsync(int id, CancellationToken cancellationToken);
}
