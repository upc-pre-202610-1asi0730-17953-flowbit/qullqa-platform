using Flowbit.Qullqa.Platform.Products.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Products.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Products.Interfaces.Rest.Transform;

public static class UpdateMinimumStockCommandFromResourceAssembler
{
    public static UpdateMinimumStockCommand ToCommandFromResource(UpdateMinimumStockResource resource, int productId)
    {
        return new UpdateMinimumStockCommand(productId, resource.MinimumStock);
    }
}
