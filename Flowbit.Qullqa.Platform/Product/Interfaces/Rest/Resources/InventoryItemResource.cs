namespace Flowbit.Qullqa.Platform.Product.Interfaces.Rest.Resources;

public record InventoryItemResource(int Id, int ProductId, int BusinessId, int WarehouseId, int StockUnit, int MinimumStock);
