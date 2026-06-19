namespace Qullqa.Platform.Sales.Interfaces.Rest.Resources;

public record CreateCustomerResource(int BusinessId, string FullName, string DocumentNumber, string PhoneNumber);
