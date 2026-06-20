using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Delivery.Domain.Model.Commands;

public record UpdateDeliveryStatusCommand(int Id, DeliveryStatus Status);
