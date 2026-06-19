using Flowbit.Qullqa.Platform.Sales.Application.QueryServices;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Sales.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Sales.Application.Internal.QueryServices;

public class CustomerQueryService(ICustomerRepository customerRepository) : ICustomerQueryService
{
    public async Task<IEnumerable<Customer>> Handle(GetCustomersByBusinessQuery query, CancellationToken cancellationToken)
        => await customerRepository.FindByBusinessIdAsync(query.BusinessId, cancellationToken);
}
