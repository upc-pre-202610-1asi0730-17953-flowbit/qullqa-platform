using Qullqa.Platform.v2.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Suppliers.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Suppliers.Interfaces.Rest.Transform;

public static class PurchaseOrderResourceFromEntityAssembler
{
    public static PurchaseOrderResource ToResourceFromEntity(PurchaseOrder purchaseOrder)
    {
        var details = purchaseOrder.Details
            .Select(detail => new PurchaseOrderDetailResource(detail.Id, detail.PurchaseId, detail.ProductId, detail.Quantity,
                detail.UnitPrice, detail.Discount, detail.Subtotal, detail.DeliveryStatus, detail.DeliveryTrackingNum))
            .ToList();

        return new PurchaseOrderResource(purchaseOrder.Id, purchaseOrder.BusinessId, purchaseOrder.SupplierId,
            purchaseOrder.Date, purchaseOrder.ExpectedDate, purchaseOrder.ReceivedDate, purchaseOrder.Status,
            purchaseOrder.Currency, purchaseOrder.Description, details);
    }
}
