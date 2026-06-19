using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Commands;

public record UpdatePurchaseOrderStatusCommand(int Id, PurchaseOrderStatus Status);
