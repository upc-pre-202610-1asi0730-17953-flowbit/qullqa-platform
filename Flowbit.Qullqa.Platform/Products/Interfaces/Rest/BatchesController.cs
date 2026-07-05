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
[Route("api/v1/batches")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Product expiration batches")]
public class BatchesController(
    IBatchQueryService batchQueryService,
    IInventoryCommandService inventoryCommandService,
    ICurrentUserAccessor currentUserAccessor,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    /// <summary>Lists all batches for the business (used to compute business-wide expiration alerts), or for a single product when ?productId= is given.</summary>
    [HttpGet]
    [SwaggerOperation(Summary = "List batches", OperationId = "GetBatches")]
    public async Task<IActionResult> GetBatches([FromQuery] int? productId, CancellationToken cancellationToken)
    {
        var businessId = currentUserAccessor.CurrentBusinessId;
        if (businessId == null) return Unauthorized();

        var batches = productId.HasValue
            ? await batchQueryService.Handle(new GetAllBatchesByProductIdQuery(productId.Value), cancellationToken)
            : await batchQueryService.Handle(new GetAllBatchesByBusinessIdQuery(businessId.Value), cancellationToken);

        return Ok(batches.Select(BatchResourceFromEntityAssembler.ToResourceFromEntity));
    }

    /// <summary>Creates a batch, or updates the product's existing ACTIVE batch in place — see CreateOrUpdateBatchCommand.</summary>
    [HttpPost]
    [SwaggerOperation(Summary = "Create or update a product's active batch", OperationId = "CreateOrUpdateBatch")]
    public async Task<IActionResult> CreateOrUpdateBatch([FromBody] CreateOrUpdateBatchResource resource,
        CancellationToken cancellationToken)
    {
        var businessId = currentUserAccessor.CurrentBusinessId;
        if (businessId == null) return Unauthorized();

        var command = CreateOrUpdateBatchCommandFromResourceAssembler.ToCommandFromResource(resource, businessId.Value);
        var result = await inventoryCommandService.Handle(command, cancellationToken);

        return ProductActionResultAssembler.ToActionResult(result, problemDetailsFactory,
            batch => Ok(BatchResourceFromEntityAssembler.ToResourceFromEntity(batch)));
    }
}
