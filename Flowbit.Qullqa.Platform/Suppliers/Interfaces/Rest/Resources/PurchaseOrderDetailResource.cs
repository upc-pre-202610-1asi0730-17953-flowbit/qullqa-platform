using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;

public record PurchaseOrderDetailResource(int Id, int PurchaseId, int ProductId, string ProductName,
    int Quantity, decimal UnitPrice, decimal Discount, decimal LineTotal,
    OrderDetailDeliveryStatus DeliveryStatus, string DeliveryTrackingNum);
