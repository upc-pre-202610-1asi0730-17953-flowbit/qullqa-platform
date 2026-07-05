using Qullqa.Platform.v2.Subscription.Application.QueryServices;
using Qullqa.Platform.v2.Subscription.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Subscription.Domain.Model.Queries;
using Qullqa.Platform.v2.Subscription.Domain.Repositories;

namespace Qullqa.Platform.v2.Subscription.Application.Internal.QueryServices;

public class PlanQueryService(IPlanRepository planRepository) : IPlanQueryService
{
    public async Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query, CancellationToken cancellationToken)
    {
        return await planRepository.ListAsync(cancellationToken);
    }

    public async Task<Plan?> Handle(GetPlanByIdQuery query, CancellationToken cancellationToken)
    {
        return await planRepository.FindByIdAsync(query.PlanId, cancellationToken);
    }
}
