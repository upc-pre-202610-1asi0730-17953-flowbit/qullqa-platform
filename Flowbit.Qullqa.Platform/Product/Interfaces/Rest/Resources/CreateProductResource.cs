using Qullqa.Platform.Product.Domain.Model.Enums;

namespace Qullqa.Platform.Product.Interfaces.Rest.Resources;

public record CreateProductResource(int BusinessId, string Name, string Description, ProductCategory Category, decimal BasePrice);
