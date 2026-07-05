using Qullqa.Platform.v2.Shared.Application.Model;
using Qullqa.Platform.v2.Subscription.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Subscription.Domain.Model.Commands;

namespace Qullqa.Platform.v2.Subscription.Application.CommandServices;

public interface IPlanCommandService
{
    Task<Result<Plan>> Handle(CreatePlanCommand command, CancellationToken cancellationToken);
    Task<Result<Plan>> Handle(UpdatePlanCommand command, CancellationToken cancellationToken);
}
