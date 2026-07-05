using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Suppliers.Application.QueryServices;

public interface IPurchaseOrderQueryService
{
    Task<IEnumerable<PurchaseOrder>> Handle(GetAllPurchaseOrdersByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<PurchaseOrder>> Handle(GetPurchaseOrdersBySupplierIdQuery query, CancellationToken cancellationToken);
    Task<PurchaseOrder?> Handle(GetPurchaseOrderByIdQuery query, CancellationToken cancellationToken);
}
