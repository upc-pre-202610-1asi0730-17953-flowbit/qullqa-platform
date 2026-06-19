using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Shared.Domain.Repositories;

namespace Qullqa.Platform.Iam.Domain.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken);
}
