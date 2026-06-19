using Flowbit.Qullqa.Platform.Product.Interfaces.Rest.Resources;

namespace Flowbit.Qullqa.Platform.Product.Interfaces.Rest.Transform;

public static class ProductResourceFromEntityAssembler
{
    public static ProductResource ToResourceFromEntity(Domain.Model.Aggregates.Product p)
        => new(p.Id, p.BusinessId, p.Name, p.Description, p.Category, p.BasePrice, p.Status);
}
