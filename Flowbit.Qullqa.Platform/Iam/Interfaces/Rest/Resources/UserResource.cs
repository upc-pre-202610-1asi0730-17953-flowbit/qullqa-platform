using Qullqa.Platform.Iam.Domain.Model.Enums;

namespace Qullqa.Platform.Iam.Interfaces.Rest.Resources;

public record UserResource(int Id, string Email, string Name, string LastName, int? BusinessId, int? RoleId, UserStatus Status);
