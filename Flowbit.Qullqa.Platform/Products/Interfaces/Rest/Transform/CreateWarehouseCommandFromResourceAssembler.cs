using Qullqa.Platform.v2.Products.Domain.Model.Commands;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;

public static class CreateWarehouseCommandFromResourceAssembler
{
    public static CreateWarehouseCommand ToCommandFromResource(CreateWarehouseResource resource, int businessId)
    {
        return new CreateWarehouseCommand(businessId, resource.Name, resource.Code, resource.Address, resource.Capacity);
    }
}
