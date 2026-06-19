using Qullqa.Platform.Product.Domain.Model.Enums;

namespace Qullqa.Platform.Product.Interfaces.Rest.Resources;

public record RegisterStockMovementResource(int ProductId, int BusinessId, int Quantity, MovementType Type);
