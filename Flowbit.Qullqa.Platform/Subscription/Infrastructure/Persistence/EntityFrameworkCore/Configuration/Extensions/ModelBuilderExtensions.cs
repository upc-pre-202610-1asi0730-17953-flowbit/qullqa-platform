using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Aggregates;

namespace Flowbit.Qullqa.Platform.Subscription.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    // Default JsonSerializer escapes non-ASCII characters as \uXXXX for
    // XSS-safety — fine for HTTP responses, but MySql.EntityFrameworkCore's
    // migration SQL generator mishandles the backslash in that escape when
    // it ends up inside a seeded INSERT statement (silently drops it,
    // corrupting "á" into "u00E1"). Relaxed encoding writes the raw UTF-8
    // character instead, sidestepping the issue entirely.
    private static readonly JsonSerializerOptions FeaturesJsonOptions = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Latin1Supplement)
    };

    public static void ApplySubscriptionConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Plan>(entity =>
        {
            entity.HasKey(plan => plan.Id);
            entity.Property(plan => plan.Id).ValueGeneratedOnAdd();
            entity.Property(plan => plan.Name).IsRequired().HasMaxLength(100);
            entity.Property(plan => plan.Description).HasMaxLength(255);
            entity.Property(plan => plan.Price).HasColumnType("decimal(10,2)");
            entity.Property(plan => plan.Currency).IsRequired().HasMaxLength(10);
            entity.Property(plan => plan.TimeLength).IsRequired().HasMaxLength(20);
            entity.Property(plan => plan.Status).IsRequired().HasMaxLength(20);

            // Features is free-text, display-only marketing content (§6.9) —
            // stored as a JSON array rather than a child table since it's
            // never queried/filtered, only ever displayed as-is.
            var featuresComparer = new ValueComparer<IReadOnlyCollection<string>>(
                (left, right) => (left ?? Array.Empty<string>()).SequenceEqual(right ?? Array.Empty<string>()),
                features => features.Aggregate(0, (hash, feature) => HashCode.Combine(hash, feature.GetHashCode())),
                features => features.ToList());

            entity.Property(plan => plan.Features)
                .HasConversion(
                    features => JsonSerializer.Serialize(features, FeaturesJsonOptions),
                    json => JsonSerializer.Deserialize<List<string>>(json, FeaturesJsonOptions) ?? new List<string>(),
                    featuresComparer)
                .HasColumnType("json");

            // Seed the same 3 real plans already in server/db.json, so the
            // frontend's Settings plan-switcher shows identical data to what
            // it showed against the mock.
            // HasData validates seed values against the CLR property type
            // (IReadOnlyCollection<string>), not the converted provider type —
            // pass plain string arrays here and let the configured
            // HasConversion above serialize them to JSON at migration time.
            entity.HasData(
                new
                {
                    Id = 1,
                    Name = "Plan Básico",
                    Description = "Ideal para pequeñas bodegas y farmacias",
                    Price = 19.9m,
                    Currency = "PEN",
                    TimeLength = PlanTimeLength.Monthly,
                    Status = PlanStatus.Active,
                    Features = (IReadOnlyCollection<string>) new[] { "Inventario básico", "Ventas POS", "1 almacén", "Hasta 100 productos" }
                },
                new
                {
                    Id = 2,
                    Name = "Plan Pro",
                    Description = "Para negocios en crecimiento",
                    Price = 49.9m,
                    Currency = "PEN",
                    TimeLength = PlanTimeLength.Monthly,
                    Status = PlanStatus.Active,
                    Features = (IReadOnlyCollection<string>) new[]
                    {
                        "Todo el Plan Básico", "Alertas inteligentes", "3 almacenes", "Proveedores ilimitados",
                        "Reportes avanzados", "Tracking IoT"
                    }
                },
                new
                {
                    Id = 3,
                    Name = "Plan Enterprise",
                    Description = "Para cadenas de tiendas y farmacias",
                    Price = 99.9m,
                    Currency = "PEN",
                    TimeLength = PlanTimeLength.Monthly,
                    Status = PlanStatus.Active,
                    Features = (IReadOnlyCollection<string>) new[]
                    {
                        "Todo el Plan Pro", "Almacenes ilimitados", "Soporte prioritario", "API dedicada", "Multi-negocio"
                    }
                });
        });
    }
}
