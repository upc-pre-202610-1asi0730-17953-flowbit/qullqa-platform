using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Transform;

public static class ReportResourceFromEntityAssembler
{
    public static ReportResource ToResourceFromEntity(Report r) => new(r.Id, r.BusinessId, r.Type, r.Filters, r.GeneratedAt);
}
