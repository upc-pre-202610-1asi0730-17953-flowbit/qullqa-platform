namespace Qullqa.Platform.v2.Suppliers.Domain.Model.Commands;

/// <summary>Status must be one of PurchaseOrderStatus (PENDING is never a valid target — orders start there).</summary>
public record UpdatePurchaseOrderStatusCommand(int PurchaseOrderId, string Status);
