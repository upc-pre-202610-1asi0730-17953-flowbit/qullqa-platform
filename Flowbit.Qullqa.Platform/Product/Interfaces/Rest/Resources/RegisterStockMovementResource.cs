using Flowbit.Qullqa.Platform.Product.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Product.Interfaces.Rest.Resources;

public record RegisterStockMovementResource(int ProductId, int BusinessId, int Quantity, MovementType Type);
