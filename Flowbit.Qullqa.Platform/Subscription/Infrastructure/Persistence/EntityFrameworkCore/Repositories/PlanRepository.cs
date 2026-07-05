using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Qullqa.Platform.v2.Subscription.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Subscription.Domain.Repositories;

namespace Qullqa.Platform.v2.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class PlanRepository(AppDbContext context) : BaseRepository<Plan>(context), IPlanRepository
{
}
