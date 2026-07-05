using Flowbit.Qullqa.Platform.Iam.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Transform;

public static class RoleResourceFromEntityAssembler
{
    public static RoleResource ToResourceFromEntity(Role role)
    {
        return new RoleResource(role.Id, role.Position);
    }
}
