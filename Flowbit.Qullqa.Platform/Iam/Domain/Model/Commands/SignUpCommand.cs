namespace Qullqa.Platform.Iam.Domain.Model.Commands;

public record SignUpCommand(string Email, string Password, string FirstName, string LastName);
