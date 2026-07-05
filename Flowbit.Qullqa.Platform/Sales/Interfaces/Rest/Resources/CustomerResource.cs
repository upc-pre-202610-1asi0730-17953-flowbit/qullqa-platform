namespace Qullqa.Platform.v2.Sales.Interfaces.Rest.Resources;

public record CustomerResource(int Id, int BusinessId, string FullName, string DocumentNumber, string PhoneNumber,
    string Email, DateTimeOffset RegisteredAt);
