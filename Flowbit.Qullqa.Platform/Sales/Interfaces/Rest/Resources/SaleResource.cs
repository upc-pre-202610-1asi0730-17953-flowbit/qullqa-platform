using Qullqa.Platform.Sales.Domain.Model.Enums;

namespace Qullqa.Platform.Sales.Interfaces.Rest.Resources;

public record SaleResource(int Id, int BusinessId, int? CustomerId, SaleStatus Status, decimal TotalAmount,
    PaymentMethod? PaymentMethod, DateTimeOffset Date, string Description, string Currency,
    IEnumerable<SaleDetailResource> Details);
