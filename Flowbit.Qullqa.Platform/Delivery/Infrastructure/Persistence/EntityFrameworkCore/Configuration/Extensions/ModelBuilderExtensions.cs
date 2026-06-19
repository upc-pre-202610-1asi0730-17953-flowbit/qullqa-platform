using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Aggregates;

namespace Flowbit.Qullqa.Platform.Delivery.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDeliveryConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Domain.Model.Aggregates.Delivery>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.TrackingNumber).IsRequired().HasMaxLength(50);
            entity.HasIndex(d => d.TrackingNumber).IsUnique();
            entity.Property(d => d.SupplierName).HasMaxLength(300);
            entity.Property(d => d.Origin).HasMaxLength(300);
            entity.Property(d => d.Destination).HasMaxLength(300);
            entity.Property(d => d.DriverName).HasMaxLength(200);
            entity.Property(d => d.DriverPhone).HasMaxLength(20);
            entity.Property(d => d.Vehicle).HasMaxLength(100);
            entity.Property(d => d.LicensePlate).HasMaxLength(20);
            entity.Property(d => d.Status).HasConversion<string>();
            entity.Property(d => d.CurrentLabel).HasMaxLength(300);
            entity.Property(d => d.TotalWeight).HasColumnType("decimal(10,3)");
            entity.HasMany(d => d.Waypoints).WithOne().HasForeignKey(w => w.DeliveryId);
        });

        builder.Entity<Waypoint>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(w => w.Label).HasMaxLength(300);
        });
    }
}
