using Qullqa.Platform.Iam.Application.CommandServices;
using Qullqa.Platform.Iam.Domain.Model;
using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Iam.Domain.Model.Commands;
using Qullqa.Platform.Iam.Domain.Repositories;
using Qullqa.Platform.Shared.Application.Model;
using Qullqa.Platform.Shared.Domain.Repositories;

namespace Qullqa.Platform.Iam.Application.Internal.CommandServices;

public class RoleCommandService(IRoleRepository roleRepository, IUnitOfWork unitOfWork) : IRoleCommandService
{
    public async Task<Result<Role>> Handle(CreateRoleCommand command, CancellationToken cancellationToken)
    {
        if (await roleRepository.ExistsByNameAsync(command.Name, cancellationToken))
            return Result<Role>.Failure(IamError.DatabaseError, $"Role '{command.Name}' already exists.");

        var role = new Role(command.Name, command.Description);
        await roleRepository.AddAsync(role, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Role>.Success(role);
    }
}
