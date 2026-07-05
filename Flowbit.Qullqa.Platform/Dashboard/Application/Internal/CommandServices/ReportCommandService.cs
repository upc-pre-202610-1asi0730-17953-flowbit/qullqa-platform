using Flowbit.Qullqa.Platform.Dashboard.Application.CommandServices;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Dashboard.Application.Internal.CommandServices;

/// <summary>Persists only the report's metadata (for history) — the figures themselves are always recomputed live, on generation and on export.</summary>
public class ReportCommandService(IReportRepository reportRepository, IUnitOfWork unitOfWork) : IReportCommandService
{
    public async Task<Result<Report>> Handle(GenerateReportCommand command, CancellationToken cancellationToken)
    {
        var report = new Report(command.BusinessId, command.Type, command.DateFrom, command.DateTo);
        await reportRepository.AddAsync(report, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Report>.Success(report);
    }
}
