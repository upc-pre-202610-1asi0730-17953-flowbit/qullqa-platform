using Qullqa.Platform.Sales.Domain.Model.Enums;

namespace Qullqa.Platform.Sales.Interfaces.Rest.Resources;

public record PaySaleResource(PaymentMethod PaymentMethod, decimal TotalAmount);
