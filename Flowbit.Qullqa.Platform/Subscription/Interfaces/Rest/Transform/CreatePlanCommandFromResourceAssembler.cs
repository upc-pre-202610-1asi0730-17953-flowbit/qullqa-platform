using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Subscription.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Subscription.Interfaces.Rest.Transform;

public static class CreatePlanCommandFromResourceAssembler
{
    public static CreatePlanCommand ToCommandFromResource(CreatePlanResource resource)
    {
        return new CreatePlanCommand(resource.Name, resource.Description, resource.Price, resource.Currency,
            resource.TimeLength, resource.Features);
    }
}
