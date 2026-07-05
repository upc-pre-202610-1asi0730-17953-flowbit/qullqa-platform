using Qullqa.Platform.v2.Suppliers.Application.QueryServices;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Queries;
using Qullqa.Platform.v2.Suppliers.Domain.Repositories;

namespace Qullqa.Platform.v2.Suppliers.Application.Internal.QueryServices;

public class SupplierQueryService(ISupplierRepository supplierRepository) : ISupplierQueryService
{
    public async Task<IEnumerable<Supplier>> Handle(GetAllSuppliersByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await supplierRepository.FindAllByBusinessIdAsync(query.BusinessId, cancellationToken);
    }

    public async Task<Supplier?> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
    {
        return await supplierRepository.FindByIdAsync(query.SupplierId, cancellationToken);
    }
}
