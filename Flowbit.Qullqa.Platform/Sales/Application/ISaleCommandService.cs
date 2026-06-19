using Flowbit.Qullqa.Platform.Sales.Domain.Model.Aggregates;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Shared.Application.Model;

namespace Flowbit.Qullqa.Platform.Sales.Application.CommandServices;

public interface ISaleCommandService
{
    Task<Result<Sale>> Handle(CreateSaleCommand command, CancellationToken cancellationToken);
    Task<Result<Sale>> Handle(AddSaleDetailCommand command, CancellationToken cancellationToken);
    Task<Result<Sale>> Handle(PaySaleCommand command, CancellationToken cancellationToken);
    Task<Result<Sale>> Handle(CancelSaleCommand command, CancellationToken cancellationToken);
}
