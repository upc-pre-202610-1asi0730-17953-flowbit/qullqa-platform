using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.v2.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Sales.Domain.Repositories;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.v2.Sales.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class CustomerRepository(AppDbContext context) : BaseRepository<Customer>(context), ICustomerRepository
{
    public async Task<IEnumerable<Customer>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default)
    {
        return await Context.Set<Customer>().Where(customer => customer.BusinessId == businessId).ToListAsync(cancellationToken);
    }
}
