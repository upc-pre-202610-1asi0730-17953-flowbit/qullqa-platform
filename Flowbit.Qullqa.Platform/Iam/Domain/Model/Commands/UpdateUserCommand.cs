using Flowbit.Qullqa.Platform.Iam.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Iam.Domain.Model.Commands;

public record UpdateUserCommand(int Id, string FirstName, string LastName, UserStatus Status, int? RoleId, int? BusinessId);
