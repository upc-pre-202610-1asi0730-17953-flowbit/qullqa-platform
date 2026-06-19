using Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.Sales.Domain.Model.Queries;

namespace Qullqa.Platform.Sales.Application.QueryServices;

public interface ICustomerQueryService
{
    Task<IEnumerable<Customer>> Handle(GetCustomersByBusinessQuery query, CancellationToken cancellationToken);
}
