using Microsoft.Extensions.Localization;
using Qullqa.Platform.v2.Products.Application.CommandServices;
using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Products.Domain.Model.Commands;
using Qullqa.Platform.v2.Products.Domain.Model.Errors;
using Qullqa.Platform.v2.Products.Domain.Repositories;
using Qullqa.Platform.v2.Products.Resources;
using Qullqa.Platform.v2.Shared.Application.Model;
using Qullqa.Platform.v2.Shared.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Application.Internal.CommandServices;

public class WarehouseCommandService(
    IWarehouseRepository warehouseRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ProductMessages> localizer)
    : IWarehouseCommandService
{
    public async Task<Result<Warehouse>> Handle(CreateWarehouseCommand command, CancellationToken cancellationToken)
    {
        var warehouse = new Warehouse(command.BusinessId, command.Name, command.Code, command.Address, command.Capacity);
        await warehouseRepository.AddAsync(warehouse, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Warehouse>.Success(warehouse);
    }

    public async Task<Result<Warehouse>> Handle(UpdateWarehouseCommand command, CancellationToken cancellationToken)
    {
        var warehouse = await warehouseRepository.FindByIdAsync(command.WarehouseId, cancellationToken);
        if (warehouse == null)
            return Result<Warehouse>.Failure(ProductError.WarehouseNotFound, localizer[nameof(ProductError.WarehouseNotFound)]);

        warehouse.UpdateDetails(command.Name, command.Code, command.Address, command.Capacity);
        if (command.Active) warehouse.Activate();
        else warehouse.Deactivate();

        warehouseRepository.Update(warehouse);
        await unitOfWork.CompleteAsync(cancellationToken);
        return Result<Warehouse>.Success(warehouse);
    }
}
