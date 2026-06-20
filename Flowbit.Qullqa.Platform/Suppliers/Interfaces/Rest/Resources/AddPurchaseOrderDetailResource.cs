namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

public record AddPurchaseOrderDetailResource(int ProductId, string ProductName, int Quantity, decimal UnitPrice, decimal Discount);
