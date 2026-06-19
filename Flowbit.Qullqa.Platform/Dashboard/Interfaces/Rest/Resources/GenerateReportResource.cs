using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Resources;

public record GenerateReportResource(int BusinessId, ReportType Type, string Filters);
