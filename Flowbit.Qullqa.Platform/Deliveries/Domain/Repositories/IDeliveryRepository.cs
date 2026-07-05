using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Deliveries.Domain.Repositories;

public interface IDeliveryRepository : IBaseRepository<Delivery>
{
    Task<IEnumerable<Delivery>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);

    /// <summary>FindByIdAsync (from IBaseRepository) does not eager-load Waypoints — use this when they're needed.</summary>
    Task<Delivery?> FindByIdWithWaypointsAsync(int id, CancellationToken cancellationToken = default);

    Task<Delivery?> FindByPurchaseDetailIdAsync(int purchaseDetailId, CancellationToken cancellationToken = default);
}
