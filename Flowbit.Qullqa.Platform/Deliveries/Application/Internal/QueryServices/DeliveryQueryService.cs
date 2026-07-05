using Flowbit.Qullqa.Platform.Deliveries.Application.QueryServices;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Deliveries.Application.Internal.QueryServices;

public class DeliveryQueryService(IDeliveryRepository deliveryRepository) : IDeliveryQueryService
{
    public async Task<IEnumerable<Delivery>> Handle(GetAllDeliveriesByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await deliveryRepository.FindAllByBusinessIdAsync(query.BusinessId, cancellationToken);
    }

    public async Task<Delivery?> Handle(GetDeliveryByIdQuery query, CancellationToken cancellationToken)
    {
        return await deliveryRepository.FindByIdWithWaypointsAsync(query.DeliveryId, cancellationToken);
    }

    public async Task<Delivery?> Handle(GetDeliveryStatusByPurchaseDetailIdQuery query, CancellationToken cancellationToken)
    {
        return await deliveryRepository.FindByPurchaseDetailIdAsync(query.PurchaseDetailId, cancellationToken);
    }
}
