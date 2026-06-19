using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Commands;

namespace Flowbit.Qullqa.Platform.Suppliers.Application.CommandServices;

public interface IPurchaseOrderCommandService
{
    Task<Result<PurchaseOrder>> Handle(CreatePurchaseOrderCommand command, CancellationToken cancellationToken);
    Task<Result<PurchaseOrder>> Handle(AddPurchaseOrderDetailCommand command, CancellationToken cancellationToken);
    Task<Result<PurchaseOrder>> Handle(UpdatePurchaseOrderStatusCommand command, CancellationToken cancellationToken);
}
