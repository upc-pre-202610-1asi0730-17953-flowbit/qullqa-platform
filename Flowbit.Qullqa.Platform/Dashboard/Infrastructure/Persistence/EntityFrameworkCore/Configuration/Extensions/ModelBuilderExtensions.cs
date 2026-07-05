using Microsoft.EntityFrameworkCore;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

namespace Flowbit.Qullqa.Platform.Dashboard.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyDashboardConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Report>(entity =>
        {
            entity.HasKey(report => report.Id);
            entity.Property(report => report.Id).ValueGeneratedOnAdd();
            entity.Property(report => report.Type).IsRequired().HasMaxLength(20);
            entity.Property(report => report.DateFrom).HasDateOnlyConversion();
            entity.Property(report => report.DateTo).HasDateOnlyConversion();

            entity.HasOne<Business>().WithMany().HasForeignKey(report => report.BusinessId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
