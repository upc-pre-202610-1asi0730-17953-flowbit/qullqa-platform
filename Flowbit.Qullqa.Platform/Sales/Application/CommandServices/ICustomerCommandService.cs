using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Sales.Application.CommandServices;

public interface ICustomerCommandService
{
    Task<Result<Customer>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken);
    Task<Result<Customer>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken);
}
