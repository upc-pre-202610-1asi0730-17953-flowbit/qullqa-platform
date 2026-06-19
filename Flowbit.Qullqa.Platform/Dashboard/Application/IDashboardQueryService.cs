using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Dashboard.Application.QueryServices;

public interface IDashboardQueryService
{
    Task<IEnumerable<Report>> Handle(GetReportsByBusinessQuery query, CancellationToken cancellationToken);
    Task<MetricsSnapshot?> Handle(GetMetricsByBusinessQuery query, CancellationToken cancellationToken);
}
