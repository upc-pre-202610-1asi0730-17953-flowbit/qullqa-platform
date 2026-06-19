using Flowbit.Qullqa.Platform.Product.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Product.Domain.Model.Commands;

public record CreateProductCommand(int BusinessId, string Name, string Description, ProductCategory Category, decimal BasePrice);
