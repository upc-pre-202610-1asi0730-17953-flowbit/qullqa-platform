using Qullqa.Platform.v2.Sales.Application.QueryServices;
using Qullqa.Platform.v2.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Sales.Domain.Model.Queries;
using Qullqa.Platform.v2.Sales.Domain.Repositories;

namespace Qullqa.Platform.v2.Sales.Application.Internal.QueryServices;

public class CustomerQueryService(ICustomerRepository customerRepository) : ICustomerQueryService
{
    public async Task<IEnumerable<Customer>> Handle(GetAllCustomersByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await customerRepository.FindAllByBusinessIdAsync(query.BusinessId, cancellationToken);
    }

    public async Task<Customer?> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
    {
        return await customerRepository.FindByIdAsync(query.CustomerId, cancellationToken);
    }
}
