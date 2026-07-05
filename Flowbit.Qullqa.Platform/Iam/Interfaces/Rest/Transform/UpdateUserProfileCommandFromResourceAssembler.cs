using Flowbit.Qullqa.Platform.Iam.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Transform;

public static class UpdateUserProfileCommandFromResourceAssembler
{
    public static UpdateUserProfileCommand ToCommandFromResource(UpdateUserProfileResource resource, int userId)
    {
        return new UpdateUserProfileCommand(userId, resource.Name, resource.LastName, resource.Phone);
    }
}
