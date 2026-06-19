using Flowbit.Qullqa.Platform.Iam.Application.QueryServices;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Iam.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Iam.Application.Internal.QueryServices;

public class BusinessQueryService(IBusinessRepository businessRepository) : IBusinessQueryService
{
    public async Task<Business?> Handle(GetBusinessByIdQuery query, CancellationToken cancellationToken)
        => await businessRepository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<Business>> Handle(GetAllBusinessesQuery query, CancellationToken cancellationToken)
        => await businessRepository.ListAsync(cancellationToken);
}
