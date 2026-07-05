using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Shared.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Domain.Repositories;

public interface IBatchRepository : IBaseRepository<Batch>
{
    Task<IEnumerable<Batch>> FindAllByProductIdAsync(int productId, CancellationToken cancellationToken = default);
    Task<Batch?> FindActiveByProductIdAsync(int productId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Batch>> FindAllByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);

    /// <summary>Every ACTIVE batch across every business — used by Alerts' expiration sweep.</summary>
    Task<IEnumerable<Batch>> FindAllActiveAsync(CancellationToken cancellationToken = default);
}
