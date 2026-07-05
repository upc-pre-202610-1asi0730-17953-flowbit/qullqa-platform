using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Alerts.Domain.Repositories;

public interface IAlertRepository : IBaseRepository<Alert>
{
    Task<IEnumerable<Alert>> FindActiveByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Alert>> FindResolvedByBusinessIdAsync(int businessId, CancellationToken cancellationToken = default);

    /// <summary>Finds a non-resolved alert of the given type for a product (and batch, when relevant) — used to decide upsert-vs-create.</summary>
    Task<Alert?> FindActiveByProductAndTypeAsync(int productId, string type, int? batchId,
        CancellationToken cancellationToken = default);
}
