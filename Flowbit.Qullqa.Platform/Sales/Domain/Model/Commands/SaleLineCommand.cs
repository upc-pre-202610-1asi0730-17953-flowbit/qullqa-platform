namespace Qullqa.Platform.v2.Sales.Domain.Model.Commands;

public record SaleLineCommand(int ProductId, int Quantity, decimal UnitPrice, decimal Discount);
