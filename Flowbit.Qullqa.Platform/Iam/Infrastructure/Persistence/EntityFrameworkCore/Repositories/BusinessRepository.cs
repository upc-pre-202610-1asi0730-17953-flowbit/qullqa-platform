using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Iam.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class BusinessRepository(AppDbContext context) : BaseRepository<Business>(context), IBusinessRepository
{
    public async Task<bool> ExistsByRucAsync(string ruc, CancellationToken cancellationToken)
        => await Context.Set<Business>().AnyAsync(b => b.Ruc == ruc, cancellationToken);
}
