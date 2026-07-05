using Flowbit.Qullqa.Platform.Iam.Application.QueryServices;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Iam.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Iam.Application.Internal.QueryServices;

public class RoleQueryService(IRoleRepository roleRepository) : IRoleQueryService
{
    public async Task<IEnumerable<Role>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
    {
        return await roleRepository.ListAsync(cancellationToken);
    }
}
