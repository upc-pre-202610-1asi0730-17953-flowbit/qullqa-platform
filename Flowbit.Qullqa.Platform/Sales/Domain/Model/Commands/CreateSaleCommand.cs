namespace Qullqa.Platform.Sales.Domain.Model.Commands;

public record CreateSaleCommand(int BusinessId, int? CustomerId, string Description, string Currency);
