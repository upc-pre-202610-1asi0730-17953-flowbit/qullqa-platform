using Qullqa.Platform.v2.Subscription.Domain.Model.Commands;
using Qullqa.Platform.v2.Subscription.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Subscription.Interfaces.Rest.Transform;

public static class CreatePlanCommandFromResourceAssembler
{
    public static CreatePlanCommand ToCommandFromResource(CreatePlanResource resource)
    {
        return new CreatePlanCommand(resource.Name, resource.Description, resource.Price, resource.Currency,
            resource.TimeLength, resource.Features);
    }
}
