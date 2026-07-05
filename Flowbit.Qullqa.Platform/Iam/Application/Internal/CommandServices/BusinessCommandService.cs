using Microsoft.Extensions.Localization;
using Flowbit.Qullqa.Platform.Iam.Application.CommandServices;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Errors;
using Flowbit.Qullqa.Platform.Iam.Domain.Repositories;
using Flowbit.Qullqa.Platform.Iam.Resources;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Iam.Application.Internal.CommandServices;

public class BusinessCommandService(
    IBusinessRepository businessRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<IamMessages> localizer)
    : IBusinessCommandService
{
    public async Task<Result<Business>> Handle(UpdateBusinessCommand command, CancellationToken cancellationToken)
    {
        var business = await businessRepository.FindByIdAsync(command.BusinessId, cancellationToken);
        if (business == null)
            return Result<Business>.Failure(IamError.BusinessNotFound, localizer[nameof(IamError.BusinessNotFound)]);

        business.UpdateProfile(command.Name, command.Type, command.Address, command.Ruc);
        if (command.PlanId.HasValue) business.UpdatePlan(command.PlanId.Value);

        businessRepository.Update(business);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Business>.Success(business);
    }
}
