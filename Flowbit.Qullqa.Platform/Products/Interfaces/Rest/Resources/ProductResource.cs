namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

public record ProductResource(int Id, int BusinessId, string Name, string Description, string Category, decimal BasePrice, string Status);
