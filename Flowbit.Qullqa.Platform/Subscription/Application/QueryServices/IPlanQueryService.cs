using Qullqa.Platform.v2.Subscription.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Subscription.Domain.Model.Queries;

namespace Qullqa.Platform.v2.Subscription.Application.QueryServices;

public interface IPlanQueryService
{
    Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query, CancellationToken cancellationToken);
    Task<Plan?> Handle(GetPlanByIdQuery query, CancellationToken cancellationToken);
}
