using Flowbit.Qullqa.Platform.Products.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Products.Application.CommandServices;

public interface IProductCommandService
{
    Task<Result<Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken);
    Task<Result<Product>> Handle(UpdateProductCommand command, CancellationToken cancellationToken);
    Task<Result> Handle(DeleteProductCommand command, CancellationToken cancellationToken);
}
