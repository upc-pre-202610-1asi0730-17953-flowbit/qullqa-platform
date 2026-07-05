using Flowbit.Qullqa.Platform.Sales.Application.QueryServices;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Sales.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Sales.Application.Internal.QueryServices;

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
