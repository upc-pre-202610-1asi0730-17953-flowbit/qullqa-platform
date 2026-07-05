using Qullqa.Platform.v2.Products.Domain.Model.Commands;
using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Shared.Application.Model;

namespace Qullqa.Platform.v2.Products.Application.CommandServices;

public interface IInventoryCommandService
{
    Task<Result<InventoryItem>> Handle(RegisterStockIntakeCommand command, CancellationToken cancellationToken);
    Task<Result<InventoryItem>> Handle(RegisterStockSaleCommand command, CancellationToken cancellationToken);
    Task<Result<InventoryItem>> Handle(UpdateMinimumStockCommand command, CancellationToken cancellationToken);
    Task<Result<Batch>> Handle(CreateOrUpdateBatchCommand command, CancellationToken cancellationToken);
}
