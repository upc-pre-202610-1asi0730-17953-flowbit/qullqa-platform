using Microsoft.Extensions.Localization;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;
using Flowbit.Qullqa.Platform.Subscription.Application.CommandServices;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Errors;
using Flowbit.Qullqa.Platform.Subscription.Domain.Repositories;
using Flowbit.Qullqa.Platform.Subscription.Resources;

namespace Flowbit.Qullqa.Platform.Subscription.Application.Internal.CommandServices;

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
