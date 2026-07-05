using Qullqa.Platform.v2.Products.Application.QueryServices;
using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;
using Qullqa.Platform.v2.Products.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Application.Internal.QueryServices;

public class InventoryQueryService(IInventoryItemRepository inventoryItemRepository) : IInventoryQueryService
{
    public async Task<IEnumerable<InventoryItem>> Handle(GetInventoryByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await inventoryItemRepository.FindAllByBusinessIdAsync(query.BusinessId, cancellationToken);
    }

    public async Task<IEnumerable<InventoryItem>> Handle(GetInventoryByProductIdQuery query, CancellationToken cancellationToken)
    {
        return await inventoryItemRepository.FindAllByProductIdAsync(query.ProductId, cancellationToken);
    }
}
