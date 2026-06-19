using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Shared.Domain.Repositories;

namespace Qullqa.Platform.Iam.Domain.Repositories;

public interface IBusinessRepository : IBaseRepository<Business>
{
    Task<bool> ExistsByRucAsync(string ruc, CancellationToken cancellationToken);
}
