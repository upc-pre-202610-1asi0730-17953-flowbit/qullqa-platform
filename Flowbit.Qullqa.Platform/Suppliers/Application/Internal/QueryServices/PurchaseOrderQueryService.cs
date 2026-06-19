using Qullqa.Platform.Suppliers.Application.QueryServices;
using Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.Suppliers.Domain.Model.Queries;
using Qullqa.Platform.Suppliers.Domain.Repositories;

namespace Qullqa.Platform.Suppliers.Application.Internal.QueryServices;

public class PurchaseOrderQueryService(IPurchaseOrderRepository purchaseOrderRepository) : IPurchaseOrderQueryService
{
    public async Task<PurchaseOrder?> Handle(GetPurchaseOrderByIdQuery query, CancellationToken cancellationToken)
        => await purchaseOrderRepository.FindByIdWithDetailsAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<PurchaseOrder>> Handle(GetPurchaseOrdersByBusinessQuery query, CancellationToken cancellationToken)
        => await purchaseOrderRepository.FindByBusinessIdAsync(query.BusinessId, cancellationToken);
}
