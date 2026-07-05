namespace Qullqa.Platform.v2.Sales.Interfaces.Rest.Resources;

public record SaleDetailResource(int Id, int SaleId, int ProductId, int Quantity, decimal UnitPrice, decimal Discount, decimal Subtotal);
