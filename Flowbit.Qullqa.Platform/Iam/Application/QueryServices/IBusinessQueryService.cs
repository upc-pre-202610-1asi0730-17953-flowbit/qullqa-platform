using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Iam.Application.QueryServices;

public interface IBusinessQueryService
{
    Task<Business?> Handle(GetBusinessByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Business>> Handle(GetAllBusinessesQuery query, CancellationToken cancellationToken);
}
