using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Product.Domain.Model.Aggregates;

namespace Flowbit.Qullqa.Platform.Product.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyProductConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Domain.Model.Aggregates.Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.Description).HasMaxLength(500);
            entity.Property(p => p.BasePrice).HasColumnType("decimal(10,2)");
            entity.Property(p => p.Category).HasConversion<string>();
            entity.Property(p => p.Status).HasConversion<string>();
        });

        builder.Entity<InventoryItem>(entity =>
        {
            entity.HasKey(i => i.Id);
            entity.Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
        });

        builder.Entity<StockMovement>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(m => m.Type).HasConversion<string>();
        });

        builder.Entity<WarehouseStock>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(w => w.Id).IsRequired().ValueGeneratedOnAdd();
        });
    }
}
