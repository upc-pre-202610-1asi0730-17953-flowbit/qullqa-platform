using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Transform;

public static class BusinessResourceFromEntityAssembler
{
    public static BusinessResource ToResourceFromEntity(Business business)
    {
        return new BusinessResource(business.Id, business.Name, business.Type, business.Address, business.Ruc,
            business.PlanId, business.UserId);
    }
}
