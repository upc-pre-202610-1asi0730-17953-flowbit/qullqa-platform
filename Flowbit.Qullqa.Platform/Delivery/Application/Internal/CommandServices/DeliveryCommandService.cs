using Flowbit.Qullqa.Platform.Delivery.Application.CommandServices;
using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Enums;
using Flowbit.Qullqa.Platform.Delivery.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Delivery.Application.Internal.CommandServices;

public class DeliveryCommandService(IDeliveryRepository deliveryRepository, IUnitOfWork unitOfWork) : IDeliveryCommandService
{
    public async Task<Result<Domain.Model.Aggregates.Delivery>> Handle(CreateDeliveryCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var delivery = new Domain.Model.Aggregates.Delivery(command.BusinessId, command.SupplierName,
                command.Origin, command.Destination, command.DriverName, command.DriverPhone,
                command.Vehicle, command.LicensePlate, command.EstimatedArrival, command.TotalWeight, command.PurchaseDetailId);
            await deliveryRepository.AddAsync(delivery, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Domain.Model.Aggregates.Delivery>.Success(delivery);
        }
        catch (Exception ex) { return Result<Domain.Model.Aggregates.Delivery>.Failure(ex.Message); }
    }

    public async Task<Result<Domain.Model.Aggregates.Delivery>> Handle(UpdateDeliveryStatusCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var delivery = await deliveryRepository.FindByIdAsync(command.Id, cancellationToken);
            if (delivery is null) return Result<Domain.Model.Aggregates.Delivery>.Failure("Delivery not found");

            switch (command.Status)
            {
                case DeliveryStatus.InTransit: delivery.StartTransit(); break;
                case DeliveryStatus.AtDestination: delivery.ArriveAtDestination(); break;
                case DeliveryStatus.Completed: delivery.Complete(); break;
                case DeliveryStatus.Cancelled: delivery.Cancel(); break;
            }

            deliveryRepository.Update(delivery);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Domain.Model.Aggregates.Delivery>.Success(delivery);
        }
        catch (Exception ex) { return Result<Domain.Model.Aggregates.Delivery>.Failure(ex.Message); }
    }

    public async Task<Result<Domain.Model.Aggregates.Delivery>> Handle(UpdateDeliveryLocationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var delivery = await deliveryRepository.FindByIdAsync(command.Id, cancellationToken);
            if (delivery is null) return Result<Domain.Model.Aggregates.Delivery>.Failure("Delivery not found");

            delivery.UpdateLocation(command.Label, command.Latitude, command.Longitude);
            deliveryRepository.Update(delivery);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Domain.Model.Aggregates.Delivery>.Success(delivery);
        }
        catch (Exception ex) { return Result<Domain.Model.Aggregates.Delivery>.Failure(ex.Message); }
    }

    public async Task<Result<Domain.Model.Aggregates.Delivery>> Handle(AddWaypointCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var delivery = await deliveryRepository.FindByIdWithWaypointsAsync(command.DeliveryId, cancellationToken);
            if (delivery is null) return Result<Domain.Model.Aggregates.Delivery>.Failure("Delivery not found");

            delivery.Waypoints.Add(new Waypoint(command.DeliveryId, command.Label, command.Latitude, command.Longitude, command.SequenceOrder));
            deliveryRepository.Update(delivery);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Domain.Model.Aggregates.Delivery>.Success(delivery);
        }
        catch (Exception ex) { return Result<Domain.Model.Aggregates.Delivery>.Failure(ex.Message); }
    }
}
