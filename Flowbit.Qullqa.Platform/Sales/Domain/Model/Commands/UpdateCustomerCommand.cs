namespace Qullqa.Platform.v2.Sales.Domain.Model.Commands;

public record UpdateCustomerCommand(int CustomerId, string FullName, string DocumentNumber, string PhoneNumber, string Email);
