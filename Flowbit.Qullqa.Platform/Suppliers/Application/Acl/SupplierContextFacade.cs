using Flowbit.Qullqa.Platform.Suppliers.Domain.Repositories;
using Flowbit.Qullqa.Platform.Suppliers.Interfaces.Acl;

namespace Flowbit.Qullqa.Platform.Suppliers.Application.Acl;

public class SupplierContextFacade(IPurchaseOrderRepository purchaseOrderRepository, ISupplierRepository supplierRepository)
    : ISupplierContextFacade
{
    public async Task<(string SupplierName, int ProductId)?> GetPurchaseOrderDetailInfo(int purchaseDetailId,
        CancellationToken cancellationToken)
    {
        var result = await purchaseOrderRepository.FindByDetailIdAsync(purchaseDetailId, cancellationToken);
        if (result == null) return null;

        var (order, detail) = result.Value;
        var supplier = await supplierRepository.FindByIdAsync(order.SupplierId, cancellationToken);
        var supplierName = supplier != null ? $"{supplier.Name} {supplier.LastName}".Trim() : string.Empty;

        return (supplierName, detail.ProductId);
    }
}
