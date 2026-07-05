using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;

namespace Qullqa.Platform.v2.Products.Application.QueryServices;

public interface IProductQueryService
{
    Task<IEnumerable<Product>> Handle(GetAllProductsByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<Product?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken);
}
