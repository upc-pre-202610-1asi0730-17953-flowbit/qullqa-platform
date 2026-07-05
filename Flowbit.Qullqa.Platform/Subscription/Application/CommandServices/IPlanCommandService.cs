using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Commands;

namespace Flowbit.Qullqa.Platform.Subscription.Application.CommandServices;

public interface IPlanCommandService
{
    Task<Result<Plan>> Handle(CreatePlanCommand command, CancellationToken cancellationToken);
    Task<Result<Plan>> Handle(UpdatePlanCommand command, CancellationToken cancellationToken);
}
