using Flowbit.Qullqa.Platform.Product.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Product.Interfaces.Rest.Resources;

public record StockMovementResource(int Id, int ProductId, int BusinessId, int Quantity, MovementType Type, DateTimeOffset RegisteredAt);
