using Flowbit.Qullqa.Platform.Delivery.Application.QueryServices;
using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Delivery.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Delivery.Application.Internal.QueryServices;

public class DeliveryQueryService(IDeliveryRepository deliveryRepository) : IDeliveryQueryService
{
    public async Task<Domain.Model.Aggregates.Delivery?> Handle(GetDeliveryByIdQuery query, CancellationToken cancellationToken)
        => await deliveryRepository.FindByIdWithWaypointsAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<Domain.Model.Aggregates.Delivery>> Handle(GetDeliveriesByBusinessQuery query, CancellationToken cancellationToken)
        => await deliveryRepository.FindByBusinessIdAsync(query.BusinessId, cancellationToken);

    public async Task<Domain.Model.Aggregates.Delivery?> Handle(GetDeliveryByTrackingNumberQuery query, CancellationToken cancellationToken)
        => await deliveryRepository.FindByTrackingNumberAsync(query.TrackingNumber, cancellationToken);
}
