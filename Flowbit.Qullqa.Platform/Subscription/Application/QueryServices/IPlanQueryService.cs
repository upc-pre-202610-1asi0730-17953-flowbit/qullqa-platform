using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Subscription.Application.QueryServices;

public interface IPlanQueryService
{
    Task<IEnumerable<Plan>> Handle(GetAllPlansQuery query, CancellationToken cancellationToken);
    Task<Plan?> Handle(GetPlanByIdQuery query, CancellationToken cancellationToken);
}
