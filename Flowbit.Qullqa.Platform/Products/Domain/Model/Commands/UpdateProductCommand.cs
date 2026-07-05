namespace Qullqa.Platform.v2.Products.Domain.Model.Commands;

public record UpdateProductCommand(int ProductId, string Name, string Description, string Category, decimal BasePrice);
