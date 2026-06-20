namespace Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Resources;

public record RecordMetricsResource(int BusinessId, int TotalProducts, int TotalSales, decimal TotalRevenue, int LowStockCount);
