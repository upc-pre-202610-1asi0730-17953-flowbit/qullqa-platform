using Flowbit.Qullqa.Platform.Products.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Products.Application.CommandServices;

public interface IWarehouseCommandService
{
    Task<Result<Warehouse>> Handle(CreateWarehouseCommand command, CancellationToken cancellationToken);
    Task<Result<Warehouse>> Handle(UpdateWarehouseCommand command, CancellationToken cancellationToken);
}
