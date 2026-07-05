using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Deliveries.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DeliveryRepository(AppDbContext context) : BaseRepository<Delivery>(context), IDeliveryRepository
{
    public async Task<IEnumerable<Delivery>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Delivery>().Include(delivery => delivery.Waypoints)
            .Where(delivery => delivery.BusinessId == businessId).ToListAsync(cancellationToken);
    }

    public async Task<Delivery?> FindByIdWithWaypointsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Delivery>().Include(delivery => delivery.Waypoints)
            .FirstOrDefaultAsync(delivery => delivery.Id == id, cancellationToken);
    }

    public async Task<Delivery?> FindByPurchaseDetailIdAsync(int purchaseDetailId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Delivery>().Include(delivery => delivery.Waypoints)
            .FirstOrDefaultAsync(delivery => delivery.PurchaseDetailId == purchaseDetailId, cancellationToken);
    }
}
