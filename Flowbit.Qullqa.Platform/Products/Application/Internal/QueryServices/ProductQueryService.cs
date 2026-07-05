using Qullqa.Platform.v2.Products.Application.QueryServices;
using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;
using Qullqa.Platform.v2.Products.Domain.Repositories;

namespace Qullqa.Platform.v2.Products.Application.Internal.QueryServices;

public class ProductQueryService(IProductRepository productRepository) : IProductQueryService
{
    public async Task<IEnumerable<Product>> Handle(GetAllProductsByBusinessIdQuery query, CancellationToken cancellationToken)
    {
        return await productRepository.FindAllByBusinessIdAsync(query.BusinessId, query.Category, cancellationToken);
    }

    public async Task<Product?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        return await productRepository.FindByIdAsync(query.ProductId, cancellationToken);
    }
}
