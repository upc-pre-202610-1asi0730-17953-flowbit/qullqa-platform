namespace Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Errors;

public enum SuppliersError
{
    SupplierNotFound,
    PurchaseOrderNotFound,
    InvalidStatusTransition,
    EmptyPurchaseOrderLines,
    DatabaseError
}
