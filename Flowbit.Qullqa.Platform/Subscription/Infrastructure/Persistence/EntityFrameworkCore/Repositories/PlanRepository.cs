using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Subscription.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class PlanRepository(AppDbContext context) : BaseRepository<Plan>(context), IPlanRepository
{
}
