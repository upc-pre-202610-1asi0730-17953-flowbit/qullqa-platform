using Qullqa.Platform.v2.Sales.Domain.Model.Aggregates;
using Qullqa.Platform.v2.Sales.Domain.Model.Commands;
using Qullqa.Platform.v2.Shared.Application.Model;

namespace Qullqa.Platform.v2.Sales.Application.CommandServices;

public interface ISaleCommandService
{
    Task<Result<Sale>> Handle(CreateSaleCommand command, CancellationToken cancellationToken);
    Task<Result<Sale>> Handle(UpdateSaleStatusCommand command, CancellationToken cancellationToken);
}
