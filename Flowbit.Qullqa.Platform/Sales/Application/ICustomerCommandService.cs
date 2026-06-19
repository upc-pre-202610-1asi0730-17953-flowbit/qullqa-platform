using Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.Sales.Domain.Model.Commands;
using Qullqa.Platform.Shared.Application.Model;

namespace Qullqa.Platform.Sales.Application.CommandServices;

public interface ICustomerCommandService
{
    Task<Result<Customer>> Handle(CreateCustomerCommand command, CancellationToken cancellationToken);
}
