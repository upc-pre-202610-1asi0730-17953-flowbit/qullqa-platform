namespace Qullqa.Platform.v2.Suppliers.Domain.Model.Errors;

public enum SuppliersError
{
    SupplierNotFound,
    PurchaseOrderNotFound,
    InvalidStatusTransition,
    EmptyPurchaseOrderLines,
    DatabaseError
}
