using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Delivery.Application.CommandServices;

public interface IDeliveryCommandService
{
    Task<Result<Domain.Model.Aggregates.Delivery>> Handle(CreateDeliveryCommand command, CancellationToken cancellationToken);
    Task<Result<Domain.Model.Aggregates.Delivery>> Handle(UpdateDeliveryStatusCommand command, CancellationToken cancellationToken);
    Task<Result<Domain.Model.Aggregates.Delivery>> Handle(UpdateDeliveryLocationCommand command, CancellationToken cancellationToken);
    Task<Result<Domain.Model.Aggregates.Delivery>> Handle(AddWaypointCommand command, CancellationToken cancellationToken);
}
