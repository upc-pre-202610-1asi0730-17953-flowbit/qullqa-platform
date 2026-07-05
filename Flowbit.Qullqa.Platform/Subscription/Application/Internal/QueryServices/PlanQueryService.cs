using Flowbit.Qullqa.Platform.Subscription.Application.QueryServices;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Subscription.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Subscription.Application.Internal.QueryServices;

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
