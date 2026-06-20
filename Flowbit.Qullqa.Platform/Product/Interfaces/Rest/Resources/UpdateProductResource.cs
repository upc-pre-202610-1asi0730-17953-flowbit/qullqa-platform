using Flowbit.Qullqa.Platform.Product.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Product.Interfaces.Rest.Resources;

public record UpdateProductResource(string Name, string Description, ProductCategory Category, decimal BasePrice, ProductStatus Status);
