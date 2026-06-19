using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Transform;

public static class SaleResourceFromEntityAssembler
{
    public static SaleResource ToResourceFromEntity(Sale sale) => new(
        sale.Id, sale.BusinessId, sale.CustomerId, sale.Status, sale.TotalAmount,
        sale.PaymentMethod, sale.Date, sale.Description, sale.Currency,
        sale.Details.Select(SaleDetailResourceFromEntityAssembler.ToResourceFromEntity));
}
