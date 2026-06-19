using Qullqa.Platform.Iam.Application.QueryServices;
using Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Qullqa.Platform.Iam.Domain.Model.Queries;
using Qullqa.Platform.Iam.Domain.Repositories;

namespace Qullqa.Platform.Iam.Application.Internal.QueryServices;

public class RoleQueryService(IRoleRepository roleRepository) : IRoleQueryService
{
    public async Task<IEnumerable<Role>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken)
        => await roleRepository.ListAsync(cancellationToken);
}
