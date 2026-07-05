using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Qullqa.Platform.v2.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Qullqa.Platform.v2.Products.Application.QueryServices;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;
using Qullqa.Platform.v2.Shared.Application;
using Swashbuckle.AspNetCore.Annotations;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest;

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
