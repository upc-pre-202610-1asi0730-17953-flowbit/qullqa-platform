using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;

namespace Qullqa.Platform.v2.Products.Application.QueryServices;

public interface IInventoryQueryService
{
    Task<IEnumerable<InventoryItem>> Handle(GetInventoryByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<InventoryItem>> Handle(GetInventoryByProductIdQuery query, CancellationToken cancellationToken);
}
