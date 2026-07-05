using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Dashboard.Application.CommandServices;

public interface IReportCommandService
{
    Task<Result<Report>> Handle(GenerateReportCommand command, CancellationToken cancellationToken);
}
