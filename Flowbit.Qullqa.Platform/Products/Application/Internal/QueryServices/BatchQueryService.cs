using Flowbit.Qullqa.Platform.Products.Application.QueryServices;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Products.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Products.Application.Internal.QueryServices;

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
