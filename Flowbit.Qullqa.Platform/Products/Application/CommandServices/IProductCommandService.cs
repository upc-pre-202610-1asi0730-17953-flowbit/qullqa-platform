using Qullqa.Platform.v2.Products.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Products.Domain.Model.Commands;
using Qullqa.Platform.v2.Shared.Application.Model;

namespace Qullqa.Platform.v2.Products.Application.CommandServices;

public interface IProductCommandService
{
    Task<Result<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken);
    Task<Result<Product>> Handle(UpdateProductCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken);
}
