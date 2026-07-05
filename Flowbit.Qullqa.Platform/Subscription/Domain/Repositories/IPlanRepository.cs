using Qullqa.Platform.v2.Shared.Domain.Repositories;
using Qullqa.Platform.v2.Subscription.Domain.Model.Aggregates;

namespace Qullqa.Platform.v2.Subscription.Domain.Repositories;

/// <summary>Plan is a global catalog, not tenant-scoped — IBaseRepository's ListAsync() already returns every plan.</summary>
public interface IPlanRepository : IBaseRepository<Plan>
{
}
