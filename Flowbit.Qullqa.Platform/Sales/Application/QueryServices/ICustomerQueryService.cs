using Qullqa.Platform.v2.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Sales.Domain.Model.Queries;

namespace Qullqa.Platform.v2.Sales.Application.QueryServices;

public interface ICustomerQueryService
{
    Task<IEnumerable<Customer>> Handle(GetAllCustomersByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<Customer?> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken);
}
