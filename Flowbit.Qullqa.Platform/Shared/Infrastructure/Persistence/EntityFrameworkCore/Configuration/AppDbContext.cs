using Microsoft.EntityFrameworkCore;
//using Flowbit.Qullqa.Platform.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
//using Flowbit.Qullqa.Platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
//using Flowbit.Qullqa.Platform.Delivery.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
//using Flowbit.Qullqa.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
//using Flowbit.Qullqa.Platform.Product.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
//using Flowbit.Qullqa.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Flowbit.Qullqa.Platform.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        //builder.ApplyIamConfiguration();
        //builder.ApplyProductConfiguration();
        //builder.ApplySalesConfiguration();
        //builder.ApplySuppliersConfiguration();
        //builder.ApplyDeliveryConfiguration();
        //builder.ApplyAlertsConfiguration();
        //builder.ApplyDashboardConfiguration();

        builder.UseSnakeCaseNamingConvention();
    }
}
