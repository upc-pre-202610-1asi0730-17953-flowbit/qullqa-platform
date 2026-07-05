using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Sales.Application.QueryServices;

public interface ICustomerQueryService
{
    Task<IEnumerable<Customer>> Handle(GetAllCustomersByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<Customer?> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken);
}
