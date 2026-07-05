using Qullqa.Platform.v2.Products.Domain.Model.Commands;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;

public static class CreateProductCommandFromResourceAssembler
{
    public static CreateProductCommand ToCommandFromResource(CreateProductResource resource, int businessId)
    {
        return new CreateProductCommand(businessId, resource.Name, resource.Description, resource.Category, resource.BasePrice);
    }
}
