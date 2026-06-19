using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

public record PurchaseOrderResource(int Id, int BusinessId, int SupplierId, string SupplierName,
    DateTimeOffset Date, DateTimeOffset? ExpectedDate, DateTimeOffset? ReceivedDate,
    PurchaseOrderStatus Status, string Currency, string Description,
    IEnumerable<PurchaseOrderDetailResource> Details);
