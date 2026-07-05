using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Suppliers.Application.QueryServices;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest;

/// <summary>
///     Read-only by design, same reasoning as Sales' SaleDetailsController: a
///     purchase order's lines are always created atomically with
///     CreatePurchaseOrderCommand.
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/purchase-details")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Purchase order lines (read-only — created atomically with the order)")]
public class PurchaseDetailsController(IPurchaseOrderQueryService purchaseOrderQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "List the lines of a purchase order", OperationId = "GetPurchaseDetails")]
    public async Task<IActionResult> GetPurchaseDetails([FromQuery] int purchaseId, CancellationToken cancellationToken)
    {
        var order = await purchaseOrderQueryService.Handle(new GetPurchaseOrderByIdQuery(purchaseId), cancellationToken);
        if (order == null) return NotFound();

        var resource = PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(order);
        return Ok(resource.Details);
    }
}
