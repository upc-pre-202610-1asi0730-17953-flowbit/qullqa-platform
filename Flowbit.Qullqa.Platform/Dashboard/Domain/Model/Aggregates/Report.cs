using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Enums;
using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;

public class Report : IAuditableEntity
{
    public int Id { get; private set; }
    public int BusinessId { get; private set; }
    public ReportType Type { get; private set; }
    public string Filters { get; private set; } = string.Empty;
    public DateTimeOffset GeneratedAt { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    protected Report() { }

    public Report(int businessId, ReportType type, string filters)
    {
        BusinessId = businessId;
        Type = type;
        Filters = filters;
        GeneratedAt = DateTimeOffset.UtcNow;
    }
}
