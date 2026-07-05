using Qullqa.Platform.v2.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Queries;

namespace Qullqa.Platform.v2.Suppliers.Application.QueryServices;

public interface IPurchaseOrderQueryService
{
    Task<IEnumerable<PurchaseOrder>> Handle(GetAllPurchaseOrdersByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<PurchaseOrder>> Handle(GetPurchaseOrdersBySupplierIdQuery query, CancellationToken cancellationToken);
    Task<PurchaseOrder?> Handle(GetPurchaseOrderByIdQuery query, CancellationToken cancellationToken);
}
