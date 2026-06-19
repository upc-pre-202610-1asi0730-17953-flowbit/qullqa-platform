namespace Flowbit.Qullqa.Platform.Sales.Domain.Model.Commands;

public record AddSaleDetailCommand(int SaleId, int ProductId, int Quantity, decimal UnitPrice, decimal Discount);
