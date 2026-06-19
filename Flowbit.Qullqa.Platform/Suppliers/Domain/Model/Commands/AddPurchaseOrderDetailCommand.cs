namespace Qullqa.Platform.Suppliers.Domain.Model.Commands;

public record AddPurchaseOrderDetailCommand(int PurchaseOrderId, int ProductId, string ProductName,
    int Quantity, decimal UnitPrice, decimal Discount);
