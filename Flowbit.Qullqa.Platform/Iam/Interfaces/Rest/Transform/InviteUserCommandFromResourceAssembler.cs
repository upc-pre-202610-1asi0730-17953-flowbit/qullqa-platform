using Flowbit.Qullqa.Platform.Iam.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Transform;

public static class InviteUserCommandFromResourceAssembler
{
    public static InviteUserCommand ToCommandFromResource(InviteUserResource resource, int businessId)
    {
        return new InviteUserCommand(resource.Email, resource.Password, resource.Name, resource.LastName,
            businessId, resource.RoleId, resource.Phone);
    }
}
