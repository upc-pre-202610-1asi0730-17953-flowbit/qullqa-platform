using Flowbit.Qullqa.Platform.Sales.Application.QueryServices;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Sales.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Sales.Application.Internal.QueryServices;

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
