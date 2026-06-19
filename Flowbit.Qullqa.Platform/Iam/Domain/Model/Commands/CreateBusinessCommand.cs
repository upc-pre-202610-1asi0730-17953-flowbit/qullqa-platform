namespace Qullqa.Platform.Iam.Domain.Model.Commands;

public record CreateBusinessCommand(string Name, string Ruc, string Email, string Phone, string Address);
