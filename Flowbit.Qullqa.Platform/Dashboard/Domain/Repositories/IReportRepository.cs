using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Dashboard.Domain.Repositories;

public interface IReportRepository : IBaseRepository<Report>
{
    Task<IEnumerable<Report>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);
}
