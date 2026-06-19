using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Suppliers.Application.QueryServices;

public interface IPurchaseOrderQueryService
{
    Task<PurchaseOrder?> Handle(GetPurchaseOrderByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<PurchaseOrder>> Handle(GetPurchaseOrdersByBusinessQuery query, CancellationToken cancellationToken);
}
