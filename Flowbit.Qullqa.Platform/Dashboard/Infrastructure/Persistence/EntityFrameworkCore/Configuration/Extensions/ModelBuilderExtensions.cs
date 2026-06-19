using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Aggregates;

namespace Flowbit.Qullqa.Platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDashboardConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Report>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Filters).HasMaxLength(2000);
            entity.Property(r => r.Type).HasConversion<string>();
        });

        builder.Entity<MetricsSnapshot>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.TotalRevenue).HasColumnType("decimal(14,2)");
        });
    }
}
