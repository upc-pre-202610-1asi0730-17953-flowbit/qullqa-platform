using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;

public static class ProductResourceFromEntityAssembler
{
    public static ProductResource ToResourceFromEntity(Product product)
    {
        return new ProductResource(product.Id, product.BusinessId, product.Name, product.Description,
            product.Category, product.BasePrice, product.Status);
    }
}
