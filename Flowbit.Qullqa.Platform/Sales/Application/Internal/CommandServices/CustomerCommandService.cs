using Flowbit.Qullqa.Platform.Sales.Application.CommandServices;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Sales.Domain.Repositories;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Sales.Application.Internal.CommandServices;

public class CustomerCommandService(ICustomerRepository customerRepository, IUnitOfWork unitOfWork) : ICustomerCommandService
{
    public async Task<Result<Customer>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var customer = new Customer(command.BusinessId, command.FullName, command.DocumentNumber, command.PhoneNumber);
            await customerRepository.AddAsync(customer, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Customer>.Success(customer);
        }
        catch (Exception ex)
        {
            return Result<Customer>.Failure(ex.Message);
        }
    }
}
