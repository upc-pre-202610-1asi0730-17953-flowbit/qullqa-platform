namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

public record CreateOrUpdateBatchResource(int ProductId, DateOnly? Expiration, decimal PurchasePrice, int? InventoryId);
