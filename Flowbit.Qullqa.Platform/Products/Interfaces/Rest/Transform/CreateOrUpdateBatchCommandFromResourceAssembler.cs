using Flowbit.Qullqa.Platform.Products.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Products.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Products.Interfaces.Rest.Transform;

public static class CreateOrUpdateBatchCommandFromResourceAssembler
{
    public static CreateOrUpdateBatchCommand ToCommandFromResource(CreateOrUpdateBatchResource resource, int businessId)
    {
        return new CreateOrUpdateBatchCommand(resource.ProductId, businessId, resource.Expiration, resource.PurchasePrice,
            resource.InventoryId);
    }
}
