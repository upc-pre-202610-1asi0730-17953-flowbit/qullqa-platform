using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Entities;
using ProductAggregate = Flowbit.Qullqa.Platform.Products.Domain.Model.Aggregates.Product;

namespace Flowbit.Qullqa.Platform.Alerts.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAlertsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Alert>(entity =>
        {
            entity.HasKey(alert => alert.Id);
            entity.Property(alert => alert.Id).ValueGeneratedOnAdd();
            entity.Property(alert => alert.ProductName).HasMaxLength(150);
            entity.Property(alert => alert.Type).IsRequired().HasMaxLength(20);
            entity.Property(alert => alert.Severity).IsRequired().HasMaxLength(20);
            entity.Property(alert => alert.Message).HasMaxLength(500);
            entity.Property(alert => alert.Status).IsRequired().HasMaxLength(20);

            entity.HasOne<Business>().WithMany().HasForeignKey(alert => alert.BusinessId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne<ProductAggregate>().WithMany().HasForeignKey(alert => alert.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne<Batch>().WithMany().HasForeignKey(alert => alert.BatchId)
                .OnDelete(DeleteBehavior.SetNull).IsRequired(false);
        });

        builder.Entity<AlertRule>(entity =>
        {
            entity.HasKey(rule => rule.Id);
            entity.Property(rule => rule.Id).ValueGeneratedOnAdd();
            entity.Property(rule => rule.AlertType).IsRequired().HasMaxLength(20);

            entity.HasOne<Business>().WithMany().HasForeignKey(rule => rule.BusinessId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasIndex(rule => new { rule.BusinessId, rule.AlertType }).IsUnique();
        });
    }
}
