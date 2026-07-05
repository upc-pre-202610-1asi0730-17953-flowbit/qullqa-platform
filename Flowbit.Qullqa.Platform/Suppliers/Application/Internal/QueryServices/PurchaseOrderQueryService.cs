using Flowbit.Qullqa.Platform.Suppliers.Application.QueryServices;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Suppliers.Application.Internal.QueryServices;

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
