using Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.Sales.Interfaces.Rest.Resources;

namespace Qullqa.Platform.Sales.Interfaces.Rest.Transform;

public static class SaleDetailResourceFromEntityAssembler
{
    public static SaleDetailResource ToResourceFromEntity(SaleDetail d) => new(
        d.Id, d.SaleId, d.ProductId, d.Quantity, d.UnitPrice, d.Discount, d.LineTotal);
}
