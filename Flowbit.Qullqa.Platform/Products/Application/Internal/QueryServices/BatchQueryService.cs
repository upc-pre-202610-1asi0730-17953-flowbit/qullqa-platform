using Qullqa.Platform.v2.Products.Application.QueryServices;
using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;
using Qullqa.Platform.v2.Products.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Application.Internal.QueryServices;

public class BatchQueryService(IBatchRepository batchRepository) : IBatchQueryService
{
    public async Task<IEnumerable<Batch>> Handle(GetAllBatchesByProductIdQuery query, CancellationToken cancellationToken)
    {
        return await batchRepository.FindAllByProductIdAsync(query.ProductId, cancellationToken);
    }

    public async Task<IEnumerable<Batch>> Handle(GetAllBatchesByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await batchRepository.FindAllByBusinessIdAsync(query.BusinessId, cancellationToken);
    }
}
