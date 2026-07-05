using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class ReportRepository(AppDbContext context) : BaseRepository<Report>(context), IReportRepository
{
    public async Task<IEnumerable<Report>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Report>().Where(report => report.BusinessId == businessId)
            .OrderByDescending(report => report.GeneratedAt).ToListAsync(cancellationToken);
    }
}
