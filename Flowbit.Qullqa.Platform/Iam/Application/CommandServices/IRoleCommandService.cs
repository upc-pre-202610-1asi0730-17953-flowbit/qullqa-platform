using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Iam.Domain.Model.Commands;
using Qullqa.Platform.Shared.Application.Model;

namespace Qullqa.Platform.Iam.Application.CommandServices;

public interface IRoleCommandService
{
    Task<Result<Role>> Handle(CreateRoleCommand command, CancellationToken cancellationToken);
}
