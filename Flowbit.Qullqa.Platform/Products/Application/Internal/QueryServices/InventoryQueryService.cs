using Flowbit.Qullqa.Platform.Products.Application.QueryServices;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Products.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Products.Application.Internal.QueryServices;

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
