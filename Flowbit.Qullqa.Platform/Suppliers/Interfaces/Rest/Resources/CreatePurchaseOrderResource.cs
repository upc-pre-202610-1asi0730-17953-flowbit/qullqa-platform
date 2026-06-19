namespace Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

public record CreatePurchaseOrderResource(int BusinessId, int SupplierId, string SupplierName,
    DateTimeOffset? ExpectedDate, string Description, string Currency = "PEN");
