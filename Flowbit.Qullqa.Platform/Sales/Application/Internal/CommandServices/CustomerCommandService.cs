using Microsoft.Extensions.Localization;
using Flowbit.Qullqa.Platform.Sales.Application.CommandServices;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Errors;
using Flowbit.Qullqa.Platform.Sales.Domain.Repositories;
using Flowbit.Qullqa.Platform.Sales.Resources;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Sales.Application.Internal.CommandServices;

public class CustomerCommandService(
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<SalesMessages> localizer)
    : ICustomerCommandService
{
    public async Task<Result<Customer>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = new Customer(command.BusinessId, command.FullName, command.DocumentNumber, command.PhoneNumber,
            command.Email);
        await customerRepository.AddAsync(customer, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Customer>.Success(customer);
    }

    public async Task<Result<Customer>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.FindByIdAsync(command.CustomerId, cancellationToken);
        if (customer == null)
            return Result<Customer>.Failure(SalesError.CustomerNotFound, localizer[nameof(SalesError.CustomerNotFound)]);

        customer.UpdateDetails(command.FullName, command.DocumentNumber, command.PhoneNumber, command.Email);
        customerRepository.Update(customer);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Customer>.Success(customer);
    }

    public async Task<Result> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.FindByIdAsync(command.CustomerId, cancellationToken);
        if (customer == null)
            return Result.Failure(SalesError.CustomerNotFound, localizer[nameof(SalesError.CustomerNotFound)]);

        customerRepository.Remove(customer);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result.Success();
    }
}
