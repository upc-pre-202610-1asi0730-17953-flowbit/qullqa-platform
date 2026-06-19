using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Transform;

public static class PurchaseOrderResourceFromEntityAssembler
{
    public static PurchaseOrderResource ToResourceFromEntity(PurchaseOrder o) => new(
        o.Id, o.BusinessId, o.SupplierId, o.SupplierName, o.Date, o.ExpectedDate, o.ReceivedDate,
        o.Status, o.Currency, o.Description,
        o.Details.Select(d => new PurchaseOrderDetailResource(d.Id, d.PurchaseOrderId, d.ProductId, d.ProductName,
            d.Quantity, d.UnitPrice, d.Discount, d.LineTotal, d.DeliveryStatus, d.DeliveryTrackingNumber)));
}
