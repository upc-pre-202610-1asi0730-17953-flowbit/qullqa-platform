using Flowbit.Qullqa.Platform.Iam.Application.QueryServices;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Iam.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Iam.Application.Internal.QueryServices;

public class BusinessQueryService(IBusinessRepository businessRepository) : IBusinessQueryService
{
    public async Task<Business?> Handle(GetBusinessByIdQuery query, CancellationToken cancellationToken)
    {
        return await businessRepository.FindByIdAsync(query.BusinessId, cancellationToken);
    }
}
