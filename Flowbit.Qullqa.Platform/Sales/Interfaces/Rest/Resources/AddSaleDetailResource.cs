namespace Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Resources;

public record AddSaleDetailResource(int ProductId, int Quantity, decimal UnitPrice, decimal Discount);
