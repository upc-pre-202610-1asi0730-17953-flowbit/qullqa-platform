using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Iam.Domain.Repositories;

public interface IBusinessRepository : IBaseRepository<Business>
{
    Task<bool> ExistsByRucAsync(string ruc, CancellationToken cancellationToken);
}
