using Flowbit.Qullqa.Platform.Shared.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;

public class PurchaseOrder : IAuditableEntity
{
    public int Id { get; private set; }
    public int BusinessId { get; private set; }
    public int SupplierId { get; private set; }
    public string SupplierName { get; private set; } = string.Empty;
    public DateTimeOffset Date { get; private set; }
    public DateTimeOffset? ExpectedDate { get; private set; }
    public DateTimeOffset? ReceivedDate { get; private set; }
    public PurchaseOrderStatus Status { get; private set; } = PurchaseOrderStatus.Generated;
    public string Currency { get; private set; } = "PEN";
    public string Description { get; private set; } = string.Empty;
    public ICollection<PurchaseOrderDetail> Details { get; private set; } = new List<PurchaseOrderDetail>();
    public DateTimeOffset? CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    protected PurchaseOrder() { }

    public PurchaseOrder(int businessId, int supplierId, string supplierName, DateTimeOffset? expectedDate, string description, string currency = "PEN")
    {
        BusinessId = businessId;
        SupplierId = supplierId;
        SupplierName = supplierName;
        ExpectedDate = expectedDate;
        Description = description;
        Currency = currency;
        Date = DateTimeOffset.UtcNow;
    }

    public void Receive()
    {
        Status = PurchaseOrderStatus.Received;
        ReceivedDate = DateTimeOffset.UtcNow;
    }

    public void MarkDelayed() => Status = PurchaseOrderStatus.Delayed;
    public void Cancel() => Status = PurchaseOrderStatus.Cancelled;
}
