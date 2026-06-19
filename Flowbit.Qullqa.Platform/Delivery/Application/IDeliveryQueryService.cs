using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Delivery.Application.QueryServices;

public interface IDeliveryQueryService
{
    Task<Domain.Model.Aggregates.Delivery?> Handle(GetDeliveryByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Domain.Model.Aggregates.Delivery>> Handle(GetDeliveriesByBusinessQuery query, CancellationToken cancellationToken);
    Task<Domain.Model.Aggregates.Delivery?> Handle(GetDeliveryByTrackingNumberQuery query, CancellationToken cancellationToken);
}
