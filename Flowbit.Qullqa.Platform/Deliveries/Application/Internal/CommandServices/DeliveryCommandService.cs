using Microsoft.Extensions.Localization;
using Flowbit.Qullqa.Platform.Deliveries.Application.CommandServices;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Errors;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Repositories;
using Flowbit.Qullqa.Platform.Deliveries.Resources;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;
using Flowbit.Qullqa.Platform.Suppliers.Interfaces.Acl;

namespace Flowbit.Qullqa.Platform.Deliveries.Application.Internal.CommandServices;

public class DeliveryCommandService(
    IDeliveryRepository deliveryRepository,
    ISupplierContextFacade supplierContextFacade,
    IUnitOfWork unitOfWork,
    IStringLocalizer<DeliveryMessages> localizer)
    : IDeliveryCommandService
{
    /// <summary>When PurchaseDetailId is provided, the supplier name is autofilled via ISupplierContextFacade — see architecture doc §6.7.</summary>
    public async Task<Result<Delivery>> Handle(CreateDeliveryCommand command, CancellationToken cancellationToken)
    {
        var supplierName = command.SupplierName;
        if (command.PurchaseDetailId.HasValue)
        {
            var info = await supplierContextFacade.GetPurchaseOrderDetailInfo(command.PurchaseDetailId.Value, cancellationToken);
            if (info.HasValue) supplierName = info.Value.SupplierName;
        }

        var delivery = new Delivery(command.BusinessId, command.TrackingNumber, command.OrderId, supplierName,
            command.Origin, command.Destination, command.DriverName, command.DriverPhone, command.Vehicle,
            command.LicensePlate, command.EstimatedArrival, command.TotalWeightValue, command.TotalWeightUnit,
            command.PurchaseDetailId);

        await deliveryRepository.AddAsync(delivery, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Delivery>.Success(delivery);
    }

    public async Task<Result<Delivery>> Handle(UpdateDeliveryStatusCommand command, CancellationToken cancellationToken)
    {
        var delivery = await deliveryRepository.FindByIdAsync(command.DeliveryId, cancellationToken);
        if (delivery == null)
            return Result<Delivery>.Failure(DeliveryError.DeliveryNotFound, localizer[nameof(DeliveryError.DeliveryNotFound)]);

        delivery.UpdateStatus(command.Status, command.CurrentLabel, command.CurrentLocation);
        deliveryRepository.Update(delivery);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Delivery>.Success(delivery);
    }

    public async Task<Result<Waypoint>> Handle(RegisterWaypointCommand command, CancellationToken cancellationToken)
    {
        var delivery = await deliveryRepository.FindByIdWithWaypointsAsync(command.DeliveryId, cancellationToken);
        if (delivery == null)
            return Result<Waypoint>.Failure(DeliveryError.DeliveryNotFound, localizer[nameof(DeliveryError.DeliveryNotFound)]);

        var waypoint = delivery.AddWaypoint(command.Label, command.District, command.Location, command.SequenceOrder);
        deliveryRepository.Update(delivery);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Waypoint>.Success(waypoint);
    }

    public async Task<Result<Waypoint>> Handle(MarkWaypointReachedCommand command, CancellationToken cancellationToken)
    {
        var delivery = await deliveryRepository.FindByIdWithWaypointsAsync(command.DeliveryId, cancellationToken);
        if (delivery == null)
            return Result<Waypoint>.Failure(DeliveryError.DeliveryNotFound, localizer[nameof(DeliveryError.DeliveryNotFound)]);

        var waypoint = delivery.Waypoints.FirstOrDefault(candidate => candidate.Id == command.WaypointId);
        if (waypoint == null)
            return Result<Waypoint>.Failure(DeliveryError.WaypointNotFound, localizer[nameof(DeliveryError.WaypointNotFound)]);

        waypoint.MarkReached();
        deliveryRepository.Update(delivery);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Waypoint>.Success(waypoint);
    }
}
