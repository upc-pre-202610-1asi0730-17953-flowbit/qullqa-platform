using Flowbit.Qullqa.Platform.Products.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Products.Application.QueryServices;

public interface IInventoryQueryService
{
    Task<IEnumerable<InventoryItem>> Handle(GetInventoryByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<InventoryItem>> Handle(GetInventoryByProductIdQuery query, CancellationToken cancellationToken);
}
