using Qullqa.Platform.Sales.Application.QueryServices;
using Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.Sales.Domain.Model.Queries;
using Qullqa.Platform.Sales.Domain.Repositories;

namespace Qullqa.Platform.Sales.Application.Internal.QueryServices;

public class CustomerQueryService(ICustomerRepository customerRepository) : ICustomerQueryService
{
    public async Task<IEnumerable<Customer>> Handle(GetCustomersByBusinessQuery query, CancellationToken cancellationToken)
        => await customerRepository.FindByBusinessIdAsync(query.BusinessId, cancellationToken);
}
