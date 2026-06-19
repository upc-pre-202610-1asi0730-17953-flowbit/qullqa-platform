using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Iam.Application.CommandServices;

public interface IRoleCommandService
{
    Task<Result<Role>> Handle(CreateRoleCommand command, CancellationToken cancellationToken);
}
