using Flowbit.Qullqa.Platform.Products.Application.QueryServices;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Products.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Products.Application.Internal.QueryServices;

public class StockMovementQueryService(IStockMovementRepository stockMovementRepository) : IStockMovementQueryService
{
    public async Task<IEnumerable<StockMovement>> Handle(GetAllStockMovementsByBusinessIdQuery query,
        CancellationToken cancellationToken)
    {
        return await stockMovementRepository.FindAllByBusinessIdAsync(query.BusinessId, cancellationToken);
    }
}
