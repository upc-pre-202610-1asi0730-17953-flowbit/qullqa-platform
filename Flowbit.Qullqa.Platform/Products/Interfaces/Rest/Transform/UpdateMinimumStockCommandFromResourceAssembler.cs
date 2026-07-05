using Qullqa.Platform.v2.Products.Domain.Model.Commands;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;

public static class UpdateMinimumStockCommandFromResourceAssembler
{
    public static UpdateMinimumStockCommand ToCommandFromResource(UpdateMinimumStockResource resource, int productId)
    {
        return new UpdateMinimumStockCommand(productId, resource.MinimumStock);
    }
}
