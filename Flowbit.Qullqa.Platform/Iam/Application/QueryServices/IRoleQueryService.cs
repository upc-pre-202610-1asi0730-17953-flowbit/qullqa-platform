using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Iam.Application.QueryServices;

public interface IRoleQueryService
{
    Task<IEnumerable<Role>> Handle(GetAllRolesQuery query, CancellationToken cancellationToken);
}
