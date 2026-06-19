using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Dashboard.Domain.Repositories;

public interface IReportRepository : IBaseRepository<Report>
{
    Task<IEnumerable<Report>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
}
