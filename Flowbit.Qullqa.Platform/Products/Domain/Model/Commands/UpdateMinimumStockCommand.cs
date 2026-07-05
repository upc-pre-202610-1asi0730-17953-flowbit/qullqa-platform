namespace Qullqa.Platform.v2.Products.Domain.Model.Commands;

public record UpdateMinimumStockCommand(int ProductId, int MinimumStock);
