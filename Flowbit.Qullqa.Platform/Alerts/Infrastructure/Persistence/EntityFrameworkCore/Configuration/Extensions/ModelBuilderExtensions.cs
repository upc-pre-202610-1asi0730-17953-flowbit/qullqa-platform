using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Aggregates;

namespace Flowbit.Qullqa.Platform.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAlertsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Alert>(entity =>
        {
            entity.HasKey(a => a.Id);
            entity.Property(a => a.ProductName).HasMaxLength(300);
            entity.Property(a => a.Message).HasMaxLength(1000);
            entity.Property(a => a.Type).HasConversion<string>();
            entity.Property(a => a.Severity).HasConversion<string>();
            entity.Property(a => a.Status).HasConversion<string>();
        });
    }
}
