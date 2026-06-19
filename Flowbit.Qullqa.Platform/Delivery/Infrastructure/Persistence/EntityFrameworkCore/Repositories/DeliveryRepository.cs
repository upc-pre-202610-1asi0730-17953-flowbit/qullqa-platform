using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Delivery.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Flowbit.Qullqa.Platform.Delivery.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class DeliveryRepository(AppDbContext context) : BaseRepository<Domain.Model.Aggregates.Delivery>(context), IDeliveryRepository
{
    public async Task<IEnumerable<Domain.Model.Aggregates.Delivery>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<Domain.Model.Aggregates.Delivery>().Where(d => d.BusinessId == businessId).Include(d => d.Waypoints).ToListAsync(cancellationToken);

    public async Task<Domain.Model.Aggregates.Delivery?> FindByTrackingNumberAsync(string trackingNumber, CancellationToken cancellationToken)
        => await Context.Set<Domain.Model.Aggregates.Delivery>().Include(d => d.Waypoints).FirstOrDefaultAsync(d => d.TrackingNumber == trackingNumber, cancellationToken);

    public async Task<Domain.Model.Aggregates.Delivery?> FindByIdWithWaypointsAsync(int id, CancellationToken cancellationToken)
        => await Context.Set<Domain.Model.Aggregates.Delivery>().Include(d => d.Waypoints).FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
}
