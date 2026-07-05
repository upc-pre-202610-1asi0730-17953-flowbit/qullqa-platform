using Flowbit.Qullqa.Platform.Subscription.Domain.Repositories;
using Flowbit.Qullqa.Platform.Subscription.Interfaces.Acl;

namespace Flowbit.Qullqa.Platform.Subscription.Application.Acl;

public class SubscriptionContextFacade(IPlanRepository planRepository) : ISubscriptionContextFacade
{
    public async Task<PlanInfo?> GetPlanById(int planId, CancellationToken cancellationToken)
    {
        var plan = await planRepository.FindByIdAsync(planId, cancellationToken);
        return plan == null ? null : new PlanInfo(plan.Id, plan.Name, plan.Price, plan.Currency);
    }
}
