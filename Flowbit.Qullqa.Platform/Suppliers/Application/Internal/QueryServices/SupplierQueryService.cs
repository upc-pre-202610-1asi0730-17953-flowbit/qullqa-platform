using Qullqa.Platform.Suppliers.Application.QueryServices;
using Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.Suppliers.Domain.Model.Queries;
using Qullqa.Platform.Suppliers.Domain.Repositories;

namespace Qullqa.Platform.Suppliers.Application.Internal.QueryServices;

public class SupplierQueryService(ISupplierRepository supplierRepository) : ISupplierQueryService
{
    public async Task<Supplier?> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
        => await supplierRepository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<Supplier>> Handle(GetSuppliersByBusinessQuery query, CancellationToken cancellationToken)
        => await supplierRepository.FindByBusinessIdAsync(query.BusinessId, cancellationToken);
}
