namespace Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Commands;

public record CreatePurchaseOrderCommand(int BusinessId, int SupplierId, string SupplierName,
    DateTimeOffset? ExpectedDate, string Description, string Currency);
