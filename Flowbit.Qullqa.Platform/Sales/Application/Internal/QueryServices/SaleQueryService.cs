using Qullqa.Platform.v2.Sales.Application.QueryServices;
using Qullqa.Platform.v2.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Sales.Domain.Model.Queries;
using Qullqa.Platform.v2.Sales.Domain.Repositories;

namespace Qullqa.Platform.v2.Sales.Application.Internal.QueryServices;

public class SaleQueryService(ISaleRepository saleRepository) : ISaleQueryService
{
    public async Task<IEnumerable<Sale>> Handle(GetAllSalesByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await saleRepository.FindAllByBusinessIdAsync(query.BusinessId, query.DateFrom, query.DateTo, cancellationToken);
    }

    public async Task<Sale?> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken)
    {
        return await saleRepository.FindByIdWithDetailsAsync(query.SaleId, cancellationToken);
    }

    public async Task<decimal> Handle(GetTotalRevenueByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await saleRepository.SumPaidTotalByBusinessIdAsync(query.BusinessId, query.DateFrom, query.DateTo, cancellationToken);
    }
}
