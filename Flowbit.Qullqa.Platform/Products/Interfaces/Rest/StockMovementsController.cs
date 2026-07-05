using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Products.Application.QueryServices;
using Flowbit.Qullqa.Platform.Products.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Products.Interfaces.Rest.Transform;
using Flowbit.Qullqa.Platform.Shared.Application;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Products.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/stock-movements")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Append-only stock movement audit trail")]
public class StockMovementsController(
    IStockMovementQueryService stockMovementQueryService,
    ICurrentUserAccessor currentUserAccessor)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "List stock movements of the current business", OperationId = "GetStockMovements")]
    public async Task<IActionResult> GetStockMovements(CancellationToken cancellationToken)
    {
        var businessId = currentUserAccessor.CurrentBusinessId;
        if (businessId == null) return Unauthorized();

        var movements = await stockMovementQueryService.Handle(new GetAllStockMovementsByBusinessIdQuery(businessId.Value),
            cancellationToken);
        return Ok(movements.Select(StockMovementResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
