using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Iam.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
}
