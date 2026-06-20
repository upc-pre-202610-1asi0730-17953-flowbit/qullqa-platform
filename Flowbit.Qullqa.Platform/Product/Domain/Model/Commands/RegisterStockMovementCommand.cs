using Flowbit.Qullqa.Platform.Product.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Product.Domain.Model.Commands;

public record RegisterStockMovementCommand(int ProductId, int BusinessId, int Quantity, MovementType Type);
