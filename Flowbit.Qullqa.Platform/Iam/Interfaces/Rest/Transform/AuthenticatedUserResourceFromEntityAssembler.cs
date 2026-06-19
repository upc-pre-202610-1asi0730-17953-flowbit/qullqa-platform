using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Iam.Interfaces.Rest.Resources;

namespace Qullqa.Platform.Iam.Interfaces.Rest.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user, string token)
        => new(user.Id, user.Email, user.FirstName, user.LastName, token);
}
