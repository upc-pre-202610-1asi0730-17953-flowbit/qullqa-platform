using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Commands;

namespace Flowbit.Qullqa.Platform.Suppliers.Application.CommandServices;

public interface ISupplierCommandService
{
    Task<Result<Supplier>> Handle(CreateSupplierCommand command, CancellationToken cancellationToken);
    Task<Result<Supplier>> Handle(UpdateSupplierCommand command, CancellationToken cancellationToken);
}
