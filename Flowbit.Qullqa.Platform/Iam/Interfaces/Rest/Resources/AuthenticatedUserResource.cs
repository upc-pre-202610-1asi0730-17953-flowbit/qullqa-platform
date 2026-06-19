namespace Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Resources;

public record AuthenticatedUserResource(int Id, string Email, string FirstName, string LastName, string Token);
