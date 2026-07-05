using Qullqa.Platform.v2.Products.Application.QueryServices;
using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;
using Qullqa.Platform.v2.Products.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Application.Internal.QueryServices;

public class StockMovementQueryService(IStockMovementRepository stockMovementRepository) : IStockMovementQueryService
{
    public async Task<IEnumerable<StockMovement>> Handle(GetAllStockMovementsByBusinessIdQuery query,
        CancellationToken cancellationToken)
    {
        return await stockMovementRepository.FindAllByBusinessIdAsync(query.BusinessId, cancellationToken);
    }
}
