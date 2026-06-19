using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Iam.Domain.Model.Commands;
using Qullqa.Platform.Shared.Application.Model;

namespace Qullqa.Platform.Iam.Application.CommandServices;

public interface IUserCommandService
{
    Task<Result<(User user, string token)>> Handle(SignInCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(SignUpCommand command, CancellationToken cancellationToken);
    Task<Result<User>> Handle(UpdateUserCommand command, CancellationToken cancellationToken);
}
