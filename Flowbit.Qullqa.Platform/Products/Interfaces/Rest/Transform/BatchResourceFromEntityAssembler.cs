using Qullqa.Platform.v2.Products.Domain.Model.Entities;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;

public static class BatchResourceFromEntityAssembler
{
    public static BatchResource ToResourceFromEntity(Batch batch)
    {
        return new BatchResource(batch.Id, batch.ProductId, batch.BusinessId, batch.Expiration, batch.PurchasePrice,
            batch.Status, batch.InventoryId);
    }
}
