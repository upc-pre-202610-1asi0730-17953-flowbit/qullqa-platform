namespace Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Resources;

public record UpdateUserProfileResource(string Name, string LastName, string Phone = "");
