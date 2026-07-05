using Qullqa.Platform.v2.Shared.Application.Model;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Commands;

namespace Qullqa.Platform.v2.Suppliers.Application.CommandServices;

public interface ISupplierCommandService
{
    Task<Result<Supplier>> Handle(CreateSupplierCommand command, CancellationToken cancellationToken);
    Task<Result<Supplier>> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken);
    Task<Result<Supplier>> Handle(DeactivateSupplierCommand command, CancellationToken cancellationToken);
}
