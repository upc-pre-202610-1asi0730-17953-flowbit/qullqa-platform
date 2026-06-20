namespace Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Resources;

public record MetricsSnapshotResource(int Id, int BusinessId, int TotalProducts, int TotalSales,
    decimal InventoryValue, int LowStockProducts, int SalesCount, DateTimeOffset GeneratedAt);
