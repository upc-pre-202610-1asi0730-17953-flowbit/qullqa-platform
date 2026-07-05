using Qullqa.Platform.v2.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Queries;

namespace Qullqa.Platform.v2.Suppliers.Application.QueryServices;

public interface ISupplierQueryService
{
    Task<IEnumerable<Supplier>> Handle(GetAllSuppliersByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<Supplier?> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken);
}
