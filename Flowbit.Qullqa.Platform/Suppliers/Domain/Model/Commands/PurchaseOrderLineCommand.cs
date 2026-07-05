namespace Qullqa.Platform.v2.Suppliers.Domain.Model.Commands;

public record PurchaseOrderLineCommand(int ProductId, int Quantity, decimal UnitPrice, decimal Discount);
