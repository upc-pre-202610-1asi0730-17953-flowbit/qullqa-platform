using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Deliveries.Application.CommandServices;
using Flowbit.Qullqa.Platform.Deliveries.Application.QueryServices;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Resources;
using Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Transform;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Shared.Application;
using Flowbit.Qullqa.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/deliveries")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Shipment tracking — independent or linked to a purchase order line")]
public class DeliveriesController(
    IDeliveryCommandService deliveryCommandService,
    IDeliveryQueryService deliveryQueryService,
    ICurrentUserAccessor currentUserAccessor,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "List deliveries of the current business", OperationId = "GetDeliveries")]
    public async Task<IActionResult> GetDeliveries(CancellationToken cancellationToken)
    {
        var businessId = currentUserAccessor.CurrentBusinessId;
        if (businessId == null) return Unauthorized();

        var deliveries = await deliveryQueryService.Handle(new GetAllDeliveriesByBusinessIdQuery(businessId.Value), cancellationToken);
        return Ok(deliveries.Select(DeliveryResourceFromEntityAssembler.ToResourceFromEntity));
    }

    /// <summary>Detail includes status and last known location.</summary>
    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get a delivery by id (status + last location)", OperationId = "GetDeliveryById")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The delivery was not found")]
    public async Task<IActionResult> GetDeliveryById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var delivery = await deliveryQueryService.Handle(new GetDeliveryByIdQuery(id), cancellationToken);
        if (delivery == null || delivery.BusinessId != currentUserAccessor.CurrentBusinessId) return NotFound();

        return Ok(DeliveryResourceFromEntityAssembler.ToResourceFromEntity(delivery));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Register a delivery (independent or linked to a purchase order line)", OperationId = "CreateDelivery")]
    public async Task<IActionResult> CreateDelivery([FromBody] CreateDeliveryResource resource, CancellationToken cancellationToken)
    {
        var businessId = currentUserAccessor.CurrentBusinessId;
        if (businessId == null) return Unauthorized();

        var command = CreateDeliveryCommandFromResourceAssembler.ToCommandFromResource(resource, businessId.Value);
        var result = await deliveryCommandService.Handle(command, cancellationToken);

        return DeliveryActionResultAssembler.ToActionResult(result, problemDetailsFactory,
            delivery => CreatedAtAction(nameof(GetDeliveryById), new { id = delivery.Id },
                DeliveryResourceFromEntityAssembler.ToResourceFromEntity(delivery)));
    }

    [HttpPatch("{id:int}")]
    [SwaggerOperation(Summary = "Update a delivery's status and current location", OperationId = "UpdateDeliveryStatus")]
    public async Task<IActionResult> UpdateDeliveryStatus([FromRoute] int id, [FromBody] UpdateDeliveryStatusResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateDeliveryStatusCommandFromResourceAssembler.ToCommandFromResource(resource, id);
        var result = await deliveryCommandService.Handle(command, cancellationToken);

        return DeliveryActionResultAssembler.ToActionResult(result, problemDetailsFactory,
            delivery => Ok(DeliveryResourceFromEntityAssembler.ToResourceFromEntity(delivery)));
    }
}
