namespace Qullqa.Platform.v2.Sales.Domain.Model.Commands;

public record CreateCustomerCommand(int BusinessId, string FullName, string DocumentNumber, string PhoneNumber, string Email);
