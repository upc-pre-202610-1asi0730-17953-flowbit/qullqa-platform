using Microsoft.EntityFrameworkCore;
using Qullqa.Platform.v2.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.v2.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.v2.Deliveries.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.v2.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.v2.Products.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.v2.Sales.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.v2.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.v2.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;
using Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors;

namespace Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;

/// <summary>
///     Application database context for the Qullqa platform. A single DbContext
///     shared by every bounded context (modular monolith over one physical
///     database) — each context contributes its own ApplyXConfiguration(builder)
///     call here as it's built.
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Subscription (Plan Management) Context — configured before IAM
        // since Business.PlanId is a real FK to Plans.
        builder.ApplySubscriptionConfiguration();

        // IAM Context
        builder.ApplyIamConfiguration();

        // Product & Inventory Management Context
        builder.ApplyProductConfiguration();

        // Sales & POS Management Context
        builder.ApplySalesConfiguration();

        // Supplier & Replenishment Management Context
        builder.ApplySuppliersConfiguration();

        // Delivery Tracking Context
        builder.ApplyDeliveryConfiguration();

        // Alerts & Operational Monitoring Context
        builder.ApplyAlertsConfiguration();

        // Dashboard & Analytics Context
        builder.ApplyDashboardConfiguration();

        // General naming convention for the database objects — must run last,
        // after every bounded context has registered its entities above.
        builder.UseSnakeCaseNamingConvention();
    }
}
