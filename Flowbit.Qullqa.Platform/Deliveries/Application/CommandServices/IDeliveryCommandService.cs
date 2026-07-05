using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Deliveries.Application.CommandServices;

public interface IDeliveryCommandService
{
    Task<Result<Delivery>> Handle(CreateDeliveryCommand command, CancellationToken cancellationToken);
    Task<Result<Delivery>> Handle(UpdateDeliveryStatusCommand command, CancellationToken cancellationToken);
    Task<Result<Waypoint>> Handle(RegisterWaypointCommand command, CancellationToken cancellationToken);
    Task<Result<Waypoint>> Handle(MarkWaypointReachedCommand command, CancellationToken cancellationToken);
}
