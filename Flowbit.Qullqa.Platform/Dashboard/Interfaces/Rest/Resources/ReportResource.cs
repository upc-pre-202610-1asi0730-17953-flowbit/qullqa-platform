using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Resources;

public record ReportResource(int Id, int BusinessId, ReportType Type, string Filters, DateTimeOffset GeneratedAt);
