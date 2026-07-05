namespace Qullqa.Platform.v2.Suppliers.Interfaces.Rest.Resources;

public record PurchaseOrderLineResource(int ProductId, int Quantity, decimal UnitPrice, decimal Discount);
