using Qullqa.Platform.v2.Shared.Application.Model;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Suppliers.Domain.Model.Commands;

namespace Qullqa.Platform.v2.Suppliers.Application.CommandServices;

public interface IPurchaseOrderCommandService
{
    Task<Result<PurchaseOrder>> Handle(CreatePurchaseOrderCommand command, CancellationToken cancellationToken);
    Task<Result<PurchaseOrder>> Handle(UpdatePurchaseOrderStatusCommand command, CancellationToken cancellationToken);
}
