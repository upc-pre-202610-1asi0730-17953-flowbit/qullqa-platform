using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Aggregates;

namespace Flowbit.Qullqa.Platform.Subscription.Domain.Repositories;

/// <summary>Plan is a global catalog, not tenant-scoped — IBaseRepository's ListAsync() already returns every plan.</summary>
public interface IPlanRepository : IBaseRepository<Plan>
{
}
