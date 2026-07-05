using Microsoft.Extensions.Localization;
using Flowbit.Qullqa.Platform.Products.Application.CommandServices;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Errors;
using Flowbit.Qullqa.Platform.Products.Domain.Repositories;
using Flowbit.Qullqa.Platform.Products.Resources;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Domain.Repositories;

namespace Flowbit.Qullqa.Platform.Products.Application.Internal.CommandServices;

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
