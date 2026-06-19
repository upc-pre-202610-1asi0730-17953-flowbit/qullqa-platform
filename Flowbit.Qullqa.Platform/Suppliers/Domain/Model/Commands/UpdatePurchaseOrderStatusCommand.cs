using Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Qullqa.Platform.Suppliers.Domain.Model.Commands;

public record UpdatePurchaseOrderStatusCommand(int Id, PurchaseOrderStatus Status);
