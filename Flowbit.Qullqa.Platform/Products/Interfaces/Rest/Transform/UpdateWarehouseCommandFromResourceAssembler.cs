using Qullqa.Platform.v2.Products.Domain.Model.Commands;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;

public static class UpdateWarehouseCommandFromResourceAssembler
{
    public static UpdateWarehouseCommand ToCommandFromResource(UpdateWarehouseResource resource, int warehouseId)
    {
        return new UpdateWarehouseCommand(warehouseId, resource.Name, resource.Code, resource.Address, resource.Capacity,
            resource.Active);
    }
}
