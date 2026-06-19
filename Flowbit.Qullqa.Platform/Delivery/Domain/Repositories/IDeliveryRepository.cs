using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Delivery.Domain.Repositories;

public interface IDeliveryRepository : IBaseRepository<Domain.Model.Aggregates.Delivery>
{
    Task<IEnumerable<Domain.Model.Aggregates.Delivery>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken);
    Task<Domain.Model.Aggregates.Delivery?> FindByTrackingNumberAsync(string trackingNumber, CancellationToken cancellationToken);
    Task<Domain.Model.Aggregates.Delivery?> FindByIdWithWaypointsAsync(int id, CancellationToken cancellationToken);
}
