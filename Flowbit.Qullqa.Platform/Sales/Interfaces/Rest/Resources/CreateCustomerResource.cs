namespace Qullqa.Platform.v2.Sales.Interfaces.Rest.Resources;

public record CreateCustomerResource(string FullName, string DocumentNumber, string PhoneNumber, string Email);
