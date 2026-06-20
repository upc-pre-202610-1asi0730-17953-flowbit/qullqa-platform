using Flowbit.Qullqa.Platform.Dashboard.Application.QueryServices;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Dashboard.Application.Internal.QueryServices;

public class DashboardQueryService(IReportRepository reportRepository, IMetricsSnapshotRepository metricsSnapshotRepository) : IDashboardQueryService
{
    public async Task<IEnumerable<Report>> Handle(GetReportsByBusinessQuery query, CancellationToken cancellationToken)
        => await reportRepository.FindByBusinessIdAsync(query.BusinessId, cancellationToken);

    public async Task<MetricsSnapshot?> Handle(GetMetricsByBusinessQuery query, CancellationToken cancellationToken)
        => await metricsSnapshotRepository.FindLatestByBusinessIdAsync(query.BusinessId, cancellationToken);
}
