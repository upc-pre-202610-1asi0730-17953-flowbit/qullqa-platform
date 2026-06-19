using Flowbit.Qullqa.Platform.Suppliers.Application.QueryServices;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Suppliers.Application.Internal.QueryServices;

public class SupplierQueryService(ISupplierRepository supplierRepository) : ISupplierQueryService
{
    public async Task<Supplier?> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken)
        => await supplierRepository.FindByIdAsync(query.Id, cancellationToken);

    public async Task<IEnumerable<Supplier>> Handle(GetSuppliersByBusinessQuery query, CancellationToken cancellationToken)
        => await supplierRepository.FindByBusinessIdAsync(query.BusinessId, cancellationToken);
}
