using Flowbit.Qullqa.Platform.Product.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Product.Application.CommandServices;

public interface IProductCommandService
{
    Task<Result<Domain.Model.Aggregates.Product>> Handle(CreateProductCommand command, CancellationToken cancellationToken);
    Task<Result<Domain.Model.Aggregates.Product>> Handle(UpdateProductCommand command, CancellationToken cancellationToken);
    Task<Result<Domain.Model.Aggregates.StockMovement>> Handle(RegisterStockMovementCommand command, CancellationToken cancellationToken);
}
