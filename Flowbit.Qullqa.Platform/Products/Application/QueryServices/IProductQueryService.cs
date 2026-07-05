using Flowbit.Qullqa.Platform.Products.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Queries;

namespace Flowbit.Qullqa.Platform.Products.Application.QueryServices;

public interface IProductQueryService
{
    Task<IEnumerable<Product>> Handle(GetAllProductsByBusinessIdQuery query, CancellationToken cancellationToken);
    Task<Product?> Handle(GetProductByIdQuery query, CancellationToken cancellationToken);
}
