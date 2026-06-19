using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Iam.Domain.Repositories;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class BusinessRepository(AppDbContext context) : BaseRepository<Business>(context), IBusinessRepository
{
    public async Task<bool> ExistsByRucAsync(string ruc, CancellationToken cancellationToken)
        => await Context.Set<Business>().AnyAsync(b => b.Ruc == ruc, cancellationToken);
}
