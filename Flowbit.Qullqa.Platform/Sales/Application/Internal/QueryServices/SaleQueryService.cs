using Flowbit.Qullqa.Platform.Sales.Application.QueryServices;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Sales.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Sales.Application.Internal.QueryServices;

public class SaleQueryService(ISaleRepository saleRepository) : ISaleQueryService
{
    public async Task<Sale?> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken)
        => await saleRepository.FindByIdWithDetailsAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<Sale>> Handle(GetSalesByBusinessQuery query, CancellationToken cancellationToken)
        => await saleRepository.FindByBusinessIdAsync(query.BusinessId, cancellationToken);
}
