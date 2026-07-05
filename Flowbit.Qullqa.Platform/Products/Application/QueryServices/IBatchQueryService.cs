using Flowbit.Qullqa.Platform.Products.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Products.Application.QueryServices;

public interface IBatchQueryService
{
    Task<IEnumerable<Batch>> Handle(GetAllBatchesByProductIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Batch>> Handle(GetAllBatchesByBusinessIdQuery query, CancellationToken cancellationToken);
}
