using Qullqa.Platform.v2.Suppliers.Application.QueryServices;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Queries;
using Qullqa.Platform.v2.Suppliers.Domain.Repositories;

namespace Qullqa.Platform.v2.Suppliers.Application.Internal.QueryServices;

public class PurchaseOrderQueryService(IPurchaseOrderRepository purchaseOrderRepository) : IPurchaseOrderQueryService
{
    public async Task<IEnumerable<PurchaseOrder>> Handle(GetAllPurchaseOrdersByBusinessIdQuery query,
        CancellationToken cancellationToken)
    {
        return await purchaseOrderRepository.FindAllByBusinessIdAsync(query.BusinessId, cancellationToken);
    }

    public async Task<IEnumerable<PurchaseOrder>> Handle(GetPurchaseOrdersBySupplierIdQuery query,
        CancellationToken cancellationToken)
    {
        return await purchaseOrderRepository.FindAllBySupplierIdAsync(query.SupplierId, cancellationToken);
    }

    public async Task<PurchaseOrder?> Handle(GetPurchaseOrderByIdQuery query, CancellationToken cancellationToken)
    {
        return await purchaseOrderRepository.FindByIdWithDetailsAsync(query.PurchaseOrderId, cancellationToken);
    }
}
