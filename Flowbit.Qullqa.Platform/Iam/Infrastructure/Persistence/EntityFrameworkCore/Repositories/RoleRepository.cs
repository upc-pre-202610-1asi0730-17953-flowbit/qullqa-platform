using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Iam.Domain.Repositories;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class RoleRepository(AppDbContext context) : BaseRepository<Role>(context), IRoleRepository
{
    public async Task<bool> ExistsByNameAsync(string name, CancellationToken cancellationToken)
        => await Context.Set<Role>().AnyAsync(r => r.Name == name, cancellationToken);
}
