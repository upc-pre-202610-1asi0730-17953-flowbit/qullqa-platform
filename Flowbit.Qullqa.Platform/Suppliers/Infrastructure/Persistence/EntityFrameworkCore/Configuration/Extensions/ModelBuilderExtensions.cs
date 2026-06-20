using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;

namespace Flowbit.Qullqa.Platform.Suppliers.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySuppliersConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Supplier>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Name).IsRequired().HasMaxLength(200);
            entity.Property(s => s.LastName).HasMaxLength(200);
            entity.Property(s => s.Ruc).IsRequired().HasMaxLength(11);
            entity.HasIndex(s => s.Ruc).IsUnique();
            entity.Property(s => s.Email).HasMaxLength(200);
            entity.Property(s => s.Phone).HasMaxLength(20);
            entity.Property(s => s.Address).HasMaxLength(500);
            entity.Property(s => s.ContactPerson).HasMaxLength(200);
            entity.Property(s => s.Category).HasConversion<string>();
            entity.Property(s => s.Status).HasConversion<string>();
        });

        builder.Entity<PurchaseOrder>(entity =>
        {
            entity.HasKey(o => o.Id);
            entity.Property(o => o.SupplierName).HasMaxLength(300);
            entity.Property(o => o.Currency).HasMaxLength(10);
            entity.Property(o => o.Description).HasMaxLength(500);
            entity.Property(o => o.Status).HasConversion<string>();
            entity.HasMany(o => o.Details).WithOne().HasForeignKey(d => d.PurchaseOrderId);
        });

        builder.Entity<PurchaseOrderDetail>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.ProductName).HasMaxLength(300);
            entity.Property(d => d.UnitPrice).HasColumnType("decimal(10,2)");
            entity.Property(d => d.Discount).HasColumnType("decimal(10,2)");
            entity.Property(d => d.DeliveryStatus).HasConversion<string>();
            entity.Property(d => d.DeliveryTrackingNumber).HasMaxLength(100);
            entity.Ignore(d => d.LineTotal);
        });
    }
}
