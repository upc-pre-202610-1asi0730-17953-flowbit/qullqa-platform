using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Iam.Interfaces.Rest.Resources;

namespace Qullqa.Platform.Iam.Interfaces.Rest.Transform;

public static class RoleResourceFromEntityAssembler
{
    public static RoleResource ToResourceFromEntity(Role role)
        => new(role.Id, role.Name, role.Description);
}
