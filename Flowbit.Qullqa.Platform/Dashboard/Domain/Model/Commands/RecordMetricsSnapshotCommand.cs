namespace Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Commands;

public record RecordMetricsSnapshotCommand(int BusinessId, int TotalProducts, int TotalSales, decimal TotalRevenue, int LowStockCount);
