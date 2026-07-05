using Qullqa.Platform.v2.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Sales.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Sales.Interfaces.Rest.Transform;

public static class SaleResourceFromEntityAssembler
{
    public static SaleResource ToResourceFromEntity(Sale sale)
    {
        var details = sale.SaleDetails
            .Select(detail => new SaleDetailResource(detail.Id, detail.SaleId, detail.ProductId, detail.Quantity,
                detail.UnitPrice, detail.Discount, detail.Subtotal))
            .ToList();

        return new SaleResource(sale.Id, sale.BusinessId, sale.CustomerId, sale.Status, sale.TotalAmount,
            sale.PaymentMethod, sale.Date, sale.Description, sale.Currency, details);
    }
}
