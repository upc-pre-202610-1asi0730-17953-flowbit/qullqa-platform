using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Subscription.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Subscription.Interfaces.Rest.Transform;

public static class PlanResourceFromEntityAssembler
{
    public static PlanResource ToResourceFromEntity(Plan plan)
    {
        return new PlanResource(plan.Id, plan.Name, plan.Description, plan.Price, plan.Currency, plan.TimeLength,
            plan.Status, plan.Features);
    }
}
