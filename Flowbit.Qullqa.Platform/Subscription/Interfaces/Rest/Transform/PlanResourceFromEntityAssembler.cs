using Qullqa.Platform.v2.Subscription.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Subscription.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Subscription.Interfaces.Rest.Transform;

public static class PlanResourceFromEntityAssembler
{
    public static PlanResource ToResourceFromEntity(Plan plan)
    {
        return new PlanResource(plan.Id, plan.Name, plan.Description, plan.Price, plan.Currency, plan.TimeLength,
            plan.Status, plan.Features);
    }
}
