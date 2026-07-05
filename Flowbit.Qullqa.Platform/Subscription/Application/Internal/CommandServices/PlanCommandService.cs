using Microsoft.Extensions.Localization;
using Qullqa.Platform.v2.Shared.Application.Model;
using Qullqa.Platform.v2.Shared.Domain.Repositories;
using Qullqa.Platform.v2.Subscription.Application.CommandServices;
using Qullqa.Platform.v2.Subscription.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Subscription.Domain.Model.Commands;
using Qullqa.Platform.v2.Subscription.Domain.Model.Errors;
using Qullqa.Platform.v2.Subscription.Domain.Repositories;
using Qullqa.Platform.v2.Subscription.Resources;

namespace Qullqa.Platform.v2.Subscription.Application.Internal.CommandServices;

/// <summary>Admin/seed only — the frontend has no plan-creation UI (§6.9).</summary>
public class PlanCommandService(
    IPlanRepository planRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<SubscriptionMessages> localizer)
    : IPlanCommandService
{
    public async Task<Result<Plan>> Handle(CreatePlanCommand command, CancellationToken cancellationToken)
    {
        var plan = new Plan(command.Name, command.Description, command.Price, command.Currency, command.TimeLength,
            command.Features);
        await planRepository.AddAsync(plan, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Plan>.Success(plan);
    }

    public async Task<Result<Plan>> Handle(UpdatePlanCommand command, CancellationToken cancellationToken)
    {
        var plan = await planRepository.FindByIdAsync(command.PlanId, cancellationToken);
        if (plan == null)
            return Result<Plan>.Failure(SubscriptionError.PlanNotFound, localizer[nameof(SubscriptionError.PlanNotFound)]);

        plan.UpdateDetails(command.Name, command.Description, command.Price, command.Currency, command.TimeLength,
            command.Features);
        planRepository.Update(plan);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Plan>.Success(plan);
    }
}
