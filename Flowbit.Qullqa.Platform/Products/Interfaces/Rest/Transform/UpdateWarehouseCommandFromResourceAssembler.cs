using Flowbit.Qullqa.Platform.Products.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Products.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Products.Interfaces.Rest.Transform;

public static class UpdateWarehouseCommandFromResourceAssembler
{
    public static UpdateWarehouseCommand ToCommandFromResource(UpdateWarehouseResource resource, int warehouseId)
    {
        return new UpdateWarehouseCommand(warehouseId, resource.Name, resource.Code, resource.Address, resource.Capacity,
            resource.Active);
    }
}
