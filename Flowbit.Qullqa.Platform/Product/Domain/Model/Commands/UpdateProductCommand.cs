using Flowbit.Qullqa.Platform.Product.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Product.Domain.Model.Commands;

public record UpdateProductCommand(int Id, string Name, string Description, ProductCategory Category, decimal BasePrice, ProductStatus Status);
