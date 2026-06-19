using Flowbit.Qullqa.Platform.Iam.Application.CommandServices;
using Flowbit.Qullqa.Platform.Iam.Domain.Model;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Iam.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Iam.Application.Internal.CommandServices;

public class BusinessCommandService(IBusinessRepository businessRepository, IUnitOfWork unitOfWork) : IBusinessCommandService
{
    public async Task<Result<Business>> Handle(CreateBusinessCommand command, CancellationToken cancellationToken)
    {
        if (await businessRepository.ExistsByRucAsync(command.Ruc, cancellationToken))
            return Result<Business>.Failure(IamError.DatabaseError, $"Business with RUC '{command.Ruc}' already exists.");

        var business = new Business(command.Name, command.Ruc, command.Email, command.Phone, command.Address);
        await businessRepository.AddAsync(business, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Business>.Success(business);
    }
}
