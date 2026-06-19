using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Enums;

namespace Flowbit.Qullqa.Platform.Sales.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySalesConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.FullName).IsRequired().HasMaxLength(200);
            entity.Property(c => c.DocumentNumber).HasMaxLength(20);
            entity.Property(c => c.PhoneNumber).HasMaxLength(20);
        });

        builder.Entity<Sale>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Currency).HasMaxLength(10);
            entity.Property(s => s.Description).HasMaxLength(500);
            entity.Property(s => s.TotalAmount).HasColumnType("decimal(10,2)");
            entity.Property(s => s.Status).HasConversion<string>();
            entity.Property(s => s.PaymentMethod).HasConversion<string>();
            entity.HasMany(s => s.Details).WithOne().HasForeignKey(d => d.SaleId);
        });

        builder.Entity<SaleDetail>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.UnitPrice).HasColumnType("decimal(10,2)");
            entity.Property(d => d.Discount).HasColumnType("decimal(10,2)");
            entity.Ignore(d => d.LineTotal);
        });
    }
}
