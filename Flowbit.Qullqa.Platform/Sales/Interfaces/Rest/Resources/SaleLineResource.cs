namespace Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Resources;

public record SaleLineResource(int ProductId, int Quantity, decimal UnitPrice, decimal Discount);
