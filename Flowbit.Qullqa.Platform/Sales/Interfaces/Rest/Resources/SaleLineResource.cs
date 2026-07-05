namespace Qullqa.Platform.v2.Sales.Interfaces.Rest.Resources;

public record SaleLineResource(int ProductId, int Quantity, decimal UnitPrice, decimal Discount);
