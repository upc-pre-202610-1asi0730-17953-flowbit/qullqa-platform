using Qullqa.Platform.v2.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Sales.Domain.Model.Queries;

namespace Qullqa.Platform.v2.Sales.Application.QueryServices;

public interface ISaleQueryService
{
    Task<IEnumerable<Sale>> Handle(GetAllSalesByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<Sale?> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken);
    Task<decimal> Handle(GetTotalRevenueByBusinessIdQuery query, CancellationToken cancellationToken);
}
