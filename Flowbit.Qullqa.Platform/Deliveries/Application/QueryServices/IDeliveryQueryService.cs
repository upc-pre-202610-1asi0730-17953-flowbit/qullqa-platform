using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Deliveries.Application.QueryServices;

public interface IDeliveryQueryService
{
    Task<IEnumerable<Delivery>> Handle(GetAllDeliveriesByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<Delivery?> Handle(GetDeliveryByIdQuery query, CancellationToken cancellationToken);
    Task<Delivery?> Handle(GetDeliveryStatusByPurchaseDetailIdQuery query, CancellationToken cancellationToken);
}
