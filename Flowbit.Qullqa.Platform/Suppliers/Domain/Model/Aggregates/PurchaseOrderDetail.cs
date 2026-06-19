using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;

public class PurchaseOrderDetail : IAuditableEntity
{
    public int Id { get; private set; }
    public int PurchaseOrderId { get; private set; }
    public int ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitPrice { get; private set; }
    public decimal Discount { get; private set; }
    public OrderDetailDeliveryStatus DeliveryStatus { get; private set; } = OrderDetailDeliveryStatus.Pending;
    public string DeliveryTrackingNumber { get; private set; } = string.Empty;
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public decimal LineTotal => Quantity * UnitPrice * (1 - Discount / 100);

    protected PurchaseOrderDetail() { }

    public PurchaseOrderDetail(int purchaseOrderId, int productId, string productName, int quantity, decimal unitPrice, decimal discount)
    {
        PurchaseOrderId = purchaseOrderId;
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
        Discount = discount;
    }

    public void SetTrackingNumber(string trackingNumber) => DeliveryTrackingNumber = trackingNumber;
    public void UpdateDeliveryStatus(OrderDetailDeliveryStatus status) => DeliveryStatus = status;
}
