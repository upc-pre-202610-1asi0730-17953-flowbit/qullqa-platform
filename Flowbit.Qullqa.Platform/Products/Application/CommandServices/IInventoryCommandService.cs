using Flowbit.Qullqa.Platform.Products.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Entities;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Products.Application.CommandServices;

public interface IInventoryCommandService
{
    Task<Result<InventoryItem>> Handle(RegisterStockIntakeCommand command, CancellationToken cancellationToken);
    Task<Result<InventoryItem>> Handle(RegisterStockSaleCommand command, CancellationToken cancellationToken);
    Task<Result<InventoryItem>> Handle(UpdateMinimumStockCommand command, CancellationToken cancellationToken);
    Task<Result<Batch>> Handle(CreateOrUpdateBatchCommand command, CancellationToken cancellationToken);
}
