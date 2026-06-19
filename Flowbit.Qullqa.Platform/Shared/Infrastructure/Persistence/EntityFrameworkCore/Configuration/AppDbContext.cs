using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.Delivery.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.Product.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyIamConfiguration();
        builder.ApplyProductConfiguration();
        builder.ApplySalesConfiguration();
        builder.ApplySuppliersConfiguration();
        builder.ApplyDeliveryConfiguration();
        builder.ApplyAlertsConfiguration();
        builder.ApplyDashboardConfiguration();

        builder.UseSnakeCaseNamingConvention();
    }
}
