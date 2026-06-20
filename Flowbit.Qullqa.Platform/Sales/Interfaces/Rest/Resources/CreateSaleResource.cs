namespace Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Resources;

public record CreateSaleResource(int BusinessId, int? CustomerId, string Description, string Currency = "PEN");
