using Qullqa.Platform.v2.Subscription.Domain.Repositories;
using Qullqa.Platform.v2.Subscription.Interfaces.Acl;

namespace Qullqa.Platform.v2.Subscription.Application.Acl;

public class SubscriptionContextFacade(IPlanRepository planRepository) : ISubscriptionContextFacade
{
    public async Task<PlanInfo?> GetPlanById(int planId, CancellationToken cancellationToken)
    {
        var plan = await planRepository.FindByIdAsync(planId, cancellationToken);
        return plan == null ? null : new PlanInfo(plan.Id, plan.Name, plan.Price, plan.Currency);
    }
}
