namespace Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Resources;

public record CustomerResource(int Id, int BusinessId, string FullName, string DocumentNumber, string PhoneNumber, DateTimeOffset RegisteredAt);
