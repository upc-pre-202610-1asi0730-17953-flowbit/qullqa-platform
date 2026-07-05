namespace Qullqa.Platform.v2.Shared.Domain.Model.Entities;

/// <summary>
///     Implemented by entities that need automatic CreatedAt/UpdatedAt timestamps,
///     stamped server-side by <see cref="Qullqa.Platform.v2.Shared.Infrastructure.Persistence.EntityFrameworkCore.Interceptors.AuditableEntityInterceptor"/>
///     on every insert/update — never trusted from client input.
/// </summary>
public interface IAuditableEntity
{
    DateTimeOffset CreatedAt { get; set; }
    DateTimeOffset UpdatedAt { get; set; }
}
