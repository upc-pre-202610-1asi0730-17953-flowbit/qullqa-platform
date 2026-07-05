using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Products.Domain.Model.Commands;
using Qullqa.Platform.v2.Shared.Application.Model;

namespace Qullqa.Platform.v2.Products.Application.CommandServices;

public interface IWarehouseCommandService
{
    Task<Result<Warehouse>> Handle(CreateWarehouseCommand command, CancellationToken cancellationToken);
    Task<Result<Warehouse>> Handle(UpdateWarehouseCommand command, CancellationToken cancellationToken);
}
