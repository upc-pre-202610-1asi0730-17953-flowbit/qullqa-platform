using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Qullqa.Platform.v2.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Qullqa.Platform.v2.Products.Application.CommandServices;
using Qullqa.Platform.v2.Products.Application.QueryServices;
using Qullqa.Platform.v2.Products.Domain.Model.Queries;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Resources;
using Qullqa.Platform.v2.Products.Interfaces.Rest.Transform;
using Qullqa.Platform.v2.Shared.Application;
using Qullqa.Platform.v2.Shared.Interfaces.Rest.ProblemDetails;
using Swashbuckle.AspNetCore.Annotations;

namespace Qullqa.Platform.v2.Products.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/inventories")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Current stock per product/warehouse")]
public class InventoriesController(
    IInventoryQueryService inventoryQueryService,
    IInventoryCommandService inventoryCommandService,
    ICurrentUserAccessor currentUserAccessor,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    /// <summary>Lists inventory for the current business, or for a single product when ?productId= is given.</summary>
    [HttpGet]
    [SwaggerOperation(Summary = "List inventory items", OperationId = "GetInventoryItems")]
    public async Task<IActionResult> GetInventoryItems([FromQuery] int? productId, CancellationToken cancellationToken)
    {
        var businessId = currentUserAccessor.CurrentBusinessId;
        if (businessId == null) return Unauthorized();

        var items = productId.HasValue
            ? await inventoryQueryService.Handle(new GetInventoryByProductIdQuery(productId.Value), cancellationToken)
            : await inventoryQueryService.Handle(new GetInventoryByBusinessIdQuery(businessId.Value), cancellationToken);

        return Ok(items.Select(InventoryItemResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPatch("{productId:int}/minimum-stock")]
    [SwaggerOperation(Summary = "Update a product's minimum stock threshold", OperationId = "UpdateMinimumStock")]
    public async Task<IActionResult> UpdateMinimumStock([FromRoute] int productId, [FromBody] UpdateMinimumStockResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateMinimumStockCommandFromResourceAssembler.ToCommandFromResource(resource, productId);
        var result = await inventoryCommandService.Handle(command, cancellationToken);

        return ProductActionResultAssembler.ToActionResult(result, problemDetailsFactory,
            item => Ok(InventoryItemResourceFromEntityAssembler.ToResourceFromEntity(item)));
    }
}
