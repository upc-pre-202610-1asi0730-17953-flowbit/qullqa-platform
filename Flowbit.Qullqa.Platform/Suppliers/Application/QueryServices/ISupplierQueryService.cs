using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Suppliers.Application.QueryServices;

public interface ISupplierQueryService
{
    Task<IEnumerable<Supplier>> Handle(GetAllSuppliersByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<Supplier?> Handle(GetSupplierByIdQuery query, CancellationToken cancellationToken);
}
