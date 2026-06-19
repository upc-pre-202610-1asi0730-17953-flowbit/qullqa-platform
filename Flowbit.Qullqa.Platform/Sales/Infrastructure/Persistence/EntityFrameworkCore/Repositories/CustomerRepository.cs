using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.Sales.Domain.Repositories;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace Qullqa.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class CustomerRepository(AppDbContext context) : BaseRepository<Customer>(context), ICustomerRepository
{
    public async Task<IEnumerable<Customer>> FindByBusinessIdAsync(int businessId, CancellationToken cancellationToken)
        => await Context.Set<Customer>().Where(c => c.BusinessId == businessId).ToListAsync(cancellationToken);
}
