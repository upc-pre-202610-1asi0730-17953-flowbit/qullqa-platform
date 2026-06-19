namespace Qullqa.Platform.Shared.Domain.Model.Entities;

public interface IAuditableEntity
{
    DateTimeOffset? CreatedAt { get; }
    DateTimeOffset? UpdatedAt { get; }
}
