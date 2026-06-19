using Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.Sales.Domain.Model.Queries;

namespace Qullqa.Platform.Sales.Application.QueryServices;

public interface ISaleQueryService
{
    Task<Sale?> Handle(GetSaleByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Sale>> Handle(GetSalesByBusinessQuery query, CancellationToken cancellationToken);
}
