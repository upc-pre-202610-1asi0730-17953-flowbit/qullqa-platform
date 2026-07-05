using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Entities;

namespace Flowbit.Qullqa.Platform.Deliveries.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDeliveryConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Delivery>(entity =>
        {
            entity.HasKey(delivery => delivery.Id);
            entity.Property(delivery => delivery.Id).ValueGeneratedOnAdd();
            entity.Property(delivery => delivery.TrackingNumber).IsRequired().HasMaxLength(50);
            entity.Property(delivery => delivery.OrderId).HasMaxLength(50);
            entity.Property(delivery => delivery.SupplierName).HasMaxLength(150);
            entity.Property(delivery => delivery.Origin).HasMaxLength(255);
            entity.Property(delivery => delivery.Destination).HasMaxLength(255);
            entity.Property(delivery => delivery.DriverName).HasMaxLength(150);
            entity.Property(delivery => delivery.DriverPhone).HasMaxLength(20);
            entity.Property(delivery => delivery.Vehicle).HasMaxLength(100);
            entity.Property(delivery => delivery.LicensePlate).HasMaxLength(20);
            entity.Property(delivery => delivery.Status).IsRequired().HasMaxLength(20);
            entity.Property(delivery => delivery.CurrentLabel).HasMaxLength(255);
            entity.Property(delivery => delivery.TotalWeightValue).HasColumnType("decimal(10,2)");
            entity.Property(delivery => delivery.TotalWeightUnit).HasMaxLength(10);
            entity.Ignore(delivery => delivery.RouteProgress);

            // Explicit ToTable (rather than the default table-splitting)
            // avoids an EF Core metadata clash between this owned type and
            // Waypoint.Location — both own the same GeoCoordinate CLR type,
            // which otherwise confuses the shadow owner-key convention.
            entity.OwnsOne(delivery => delivery.CurrentLocation, location =>
            {
                location.ToTable("delivery_current_locations");
                location.Property(coordinate => coordinate.Latitude).HasColumnName("latitude");
                location.Property(coordinate => coordinate.Longitude).HasColumnName("longitude");
            });

            entity.HasOne<Business>().WithMany().HasForeignKey(delivery => delivery.BusinessId)
                .OnDelete(DeleteBehavior.Restrict);

            // Optional real FK — see architecture doc §6.7: a delivery may be
            // independent (null) or linked to a purchase order line.
            entity.HasOne<PurchaseOrderDetail>().WithMany().HasForeignKey(delivery => delivery.PurchaseDetailId)
                .OnDelete(DeleteBehavior.SetNull).IsRequired(false);

            entity.HasMany(delivery => delivery.Waypoints).WithOne().HasForeignKey(waypoint => waypoint.DeliveryId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Metadata.FindNavigation(nameof(Delivery.Waypoints))!.SetPropertyAccessMode(PropertyAccessMode.Field);
        });

        builder.Entity<Waypoint>(entity =>
        {
            entity.HasKey(waypoint => waypoint.Id);
            entity.Property(waypoint => waypoint.Id).ValueGeneratedOnAdd();
            entity.Property(waypoint => waypoint.Label).HasMaxLength(150);
            entity.Property(waypoint => waypoint.District).HasMaxLength(100);

            entity.OwnsOne(waypoint => waypoint.Location, location =>
            {
                location.ToTable("waypoint_locations");
                location.Property(coordinate => coordinate.Latitude).HasColumnName("latitude");
                location.Property(coordinate => coordinate.Longitude).HasColumnName("longitude");
            });
        });
    }
}
