using Qullqa.Platform.v2.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Sales.Domain.Model.Commands;
using Qullqa.Platform.v2.Shared.Application.Model;

namespace Qullqa.Platform.v2.Sales.Application.CommandServices;

public interface ICustomerCommandService
{
    Task<Result<Customer>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken);
    Task<Result<Customer>> Handle(UpdateCustomerCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(DeleteCustomerCommand command, CancellationToken cancellationToken);
}
