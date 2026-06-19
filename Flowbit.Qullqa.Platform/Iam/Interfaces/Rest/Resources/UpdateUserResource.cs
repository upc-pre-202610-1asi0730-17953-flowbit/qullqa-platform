using Flowbit.Qullqa.Platform.Iam.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Resources;

public record UpdateUserResource(string FirstName, string LastName, UserStatus Status, int? RoleId, int? BusinessId);
