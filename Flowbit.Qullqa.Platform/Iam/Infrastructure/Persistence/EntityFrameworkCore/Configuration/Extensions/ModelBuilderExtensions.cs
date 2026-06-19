using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;

namespace Flowbit.Qullqa.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyIamConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Role>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
            entity.Property(r => r.Description).HasMaxLength(255);
        });

        builder.Entity<Business>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(b => b.Name).IsRequired().HasMaxLength(200);
            entity.Property(b => b.Ruc).IsRequired().HasMaxLength(11);
            entity.Property(b => b.Email).HasMaxLength(200);
            entity.Property(b => b.Phone).HasMaxLength(20);
            entity.Property(b => b.Address).HasMaxLength(300);
            entity.HasIndex(b => b.Ruc).IsUnique();
        });

        builder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);
            entity.Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(200);
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.LastName).IsRequired().HasMaxLength(100);
            entity.Property(u => u.Status).HasConversion<string>();
            entity.HasIndex(u => u.Email).IsUnique();
        });
    }
}
