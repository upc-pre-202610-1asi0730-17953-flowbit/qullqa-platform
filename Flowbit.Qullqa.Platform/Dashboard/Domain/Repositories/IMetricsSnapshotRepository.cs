using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Dashboard.Domain.Repositories;

public interface IMetricsSnapshotRepository : IBaseRepository<MetricsSnapshot>
{
    Task<IEnumerable<MetricsSnapshot>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
    Task<MetricsSnapshot?> FindLatestByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
}
