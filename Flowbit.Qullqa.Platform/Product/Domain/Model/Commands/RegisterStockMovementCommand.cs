using Qullqa.Platform.Product.Domain.Model.Enums;

namespace Qullqa.Platform.Product.Domain.Model.Commands;

public record RegisterStockMovementCommand(int ProductId, int BusinessId, int Quantity, MovementType Type);
