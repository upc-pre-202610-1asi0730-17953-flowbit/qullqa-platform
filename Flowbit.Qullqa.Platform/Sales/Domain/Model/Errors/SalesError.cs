namespace Qullqa.Platform.v2.Sales.Domain.Model.Errors;

public enum SalesError
{
    SaleNotFound,
    CustomerNotFound,
    InsufficientStock,
    SaleAlreadyCancelled,
    EmptySaleLines,
    DatabaseError
}
