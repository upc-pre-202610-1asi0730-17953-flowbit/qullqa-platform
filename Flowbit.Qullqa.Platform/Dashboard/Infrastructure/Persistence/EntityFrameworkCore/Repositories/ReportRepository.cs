using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ReportRepository(AppDbContext context) : BaseRepository<Report>(context), IReportRepository
{
    public async Task<IEnumerable<Report>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<Report>().Where(r => r.BusinessId == businessId).ToListAsync(cancellationToken);
}
