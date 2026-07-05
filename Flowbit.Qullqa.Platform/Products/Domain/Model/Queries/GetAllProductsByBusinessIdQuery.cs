namespace Qullqa.Platform.v2.Products.Domain.Model.Queries;

public record GetAllProductsByBusinessIdQuery(int BusinessId, string? Category = null);
