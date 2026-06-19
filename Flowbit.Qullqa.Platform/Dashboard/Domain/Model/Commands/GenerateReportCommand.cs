using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Commands;

public record GenerateReportCommand(int BusinessId, ReportType Type, string Filters);
