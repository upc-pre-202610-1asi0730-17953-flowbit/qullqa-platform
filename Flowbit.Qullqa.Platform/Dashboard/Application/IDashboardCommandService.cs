using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Dashboard.Application.CommandServices;

public interface IDashboardCommandService
{
    Task<Result<Report>> Handle(GenerateReportCommand command, CancellationToken cancellationToken);
    Task<Result<MetricsSnapshot>> Handle(RecordMetricsSnapshotCommand command, CancellationToken cancellationToken);
}
