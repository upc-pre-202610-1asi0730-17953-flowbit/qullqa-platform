using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Iam.Domain.Repositories;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserRepository(AppDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken)
        => await Context.Set<User>().FirstOrDefaultAsync(u => u.Email == email, cancellationToken);

    public async Task<bool> ExistsByEmailAsync(string email, CancellationToken cancellationToken)
        => await Context.Set<User>().AnyAsync(u => u.Email == email, cancellationToken);

    public async Task<IEnumerable<User>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<User>().Where(u => u.BusinessId == businessId).ToListAsync(cancellationToken);
}
