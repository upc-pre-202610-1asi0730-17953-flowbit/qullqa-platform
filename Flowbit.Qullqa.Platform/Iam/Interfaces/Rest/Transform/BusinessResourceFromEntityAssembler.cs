using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Transform;

public static class BusinessResourceFromEntityAssembler
{
    public static BusinessResource ToResourceFromEntity(Business business)
        => new(business.Id, business.Name, business.Ruc, business.Email, business.Phone, business.Address);
}
