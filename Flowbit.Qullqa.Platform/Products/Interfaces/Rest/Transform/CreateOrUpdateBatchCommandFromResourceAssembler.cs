using Qullqa.Platform.v2.Products.Domain.Model.Commands;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;

public static class CreateOrUpdateBatchCommandFromResourceAssembler
{
    public static CreateOrUpdateBatchCommand ToCommandFromResource(CreateOrUpdateBatchResource resource, int businessId)
    {
        return new CreateOrUpdateBatchCommand(resource.ProductId, businessId, resource.Expiration, resource.PurchasePrice,
            resource.InventoryId);
    }
}
