using Flowbit.Qullqa.Platform.Dashboard.Application.CommandServices;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Dashboard.Application.Internal.CommandServices;

public class DashboardCommandService(IReportRepository reportRepository, IMetricsSnapshotRepository metricsSnapshotRepository, IUnitOfWork unitOfWork) : IDashboardCommandService
{
    public async Task<Result<Report>> Handle(GenerateReportCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var report = new Report(command.BusinessId, command.Type, command.Filters);
            await reportRepository.AddAsync(report, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Report>.Success(report);
        }
        catch (Exception ex) { return Result<Report>.Failure(ex.Message); }
    }

    public async Task<Result<MetricsSnapshot>> Handle(RecordMetricsSnapshotCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var snapshot = new MetricsSnapshot(command.BusinessId, command.TotalProducts, command.TotalSales, command.TotalRevenue, command.LowStockCount);
            await metricsSnapshotRepository.AddAsync(snapshot, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<MetricsSnapshot>.Success(snapshot);
        }
        catch (Exception ex) { return Result<MetricsSnapshot>.Failure(ex.Message); }
    }
}
