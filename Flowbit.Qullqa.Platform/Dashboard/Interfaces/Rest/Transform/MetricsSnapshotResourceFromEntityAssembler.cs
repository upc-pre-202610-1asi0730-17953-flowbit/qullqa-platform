using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Transform;

public static class MetricsSnapshotResourceFromEntityAssembler
{
    public static MetricsSnapshotResource ToResourceFromEntity(MetricsSnapshot m) => new(
        m.Id, m.BusinessId, m.TotalProducts, m.TotalSales, m.TotalRevenue, m.LowStockCount, 0, m.Date);
}
