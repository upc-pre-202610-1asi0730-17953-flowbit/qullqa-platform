using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;

public class MetricsSnapshot : IAuditableEntity
{
    public int Id { get; private set; }
    public int BusinessId { get; private set; }
    public int TotalProducts { get; private set; }
    public int TotalSales { get; private set; }
    public decimal TotalRevenue { get; private set; }
    public int LowStockCount { get; private set; }
    public DateTimeOffset Date { get; private set; }
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    protected MetricsSnapshot() { }

    public MetricsSnapshot(int businessId, int totalProducts, int totalSales, decimal totalRevenue, int lowStockCount)
    {
        BusinessId = businessId;
        TotalProducts = totalProducts;
        TotalSales = totalSales;
        TotalRevenue = totalRevenue;
        LowStockCount = lowStockCount;
        Date = DateTimeOffset.UtcNow;
    }
}
