using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Iam.Interfaces.Rest.Resources;

namespace Qullqa.Platform.Iam.Interfaces.Rest.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User user)
        => new(user.Id, user.Email, user.FirstName, user.LastName, user.BusinessId, user.RoleId, user.Status);
}
