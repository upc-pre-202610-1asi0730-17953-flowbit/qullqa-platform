using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Sales.Application.QueryServices;

public interface ISaleQueryService
{
    Task<IEnumerable<Sale>> Handle(GetAllSalesByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<Sale?> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken);
    Task<decimal> Handle(GetTotalRevenueByBusinessIdQuery query, CancellationToken cancellationToken);
}
