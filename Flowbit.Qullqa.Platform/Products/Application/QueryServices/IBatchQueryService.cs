using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;

namespace Qullqa.Platform.v2.Products.Application.QueryServices;

public interface IBatchQueryService
{
    Task<IEnumerable<Batch>> Handle(GetAllBatchesByProductIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Batch>> Handle(GetAllBatchesByBusinessIdQuery query, CancellationToken cancellationToken);
}
