using Flowbit.Qullqa.Platform.Sales.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Sales.Domain.Model.Commands;

public record PaySaleCommand(int SaleId, PaymentMethod PaymentMethod, decimal TotalAmount);
