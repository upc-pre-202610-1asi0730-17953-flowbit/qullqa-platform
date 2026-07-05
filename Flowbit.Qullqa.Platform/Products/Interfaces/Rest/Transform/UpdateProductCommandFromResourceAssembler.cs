using Flowbit.Qullqa.Platform.Products.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Products.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Products.Interfaces.Rest.Transform;

public static class UpdateProductCommandFromResourceAssembler
{
    public static UpdateProductCommand ToCommandFromResource(UpdateProductResource resource, int productId)
    {
        return new UpdateProductCommand(productId, resource.Name, resource.Description, resource.Category, resource.BasePrice);
    }
}
