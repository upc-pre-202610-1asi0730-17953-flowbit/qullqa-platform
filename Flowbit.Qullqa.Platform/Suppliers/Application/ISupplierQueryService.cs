using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Suppliers.Application.QueryServices;

public interface ISupplierQueryService
{
    Task<Supplier?> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Supplier>> Handle(GetSuppliersByBusinessQuery query, CancellationToken cancellationToken);
}
