using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Deliveries.Application.CommandServices;
using Flowbit.Qullqa.Platform.Deliveries.Application.QueryServices;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Resources;
using Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Transform;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/waypoints")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Stops along a delivery's route")]
public class WaypointsController(
    IDeliveryCommandService deliveryCommandService,
    IDeliveryQueryService deliveryQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "List the waypoints of a delivery", OperationId = "GetWaypoints")]
    public async Task<IActionResult> GetWaypoints([FromQuery] int deliveryId, CancellationToken cancellationToken)
    {
        var delivery = await deliveryQueryService.Handle(new GetDeliveryByIdQuery(deliveryId), cancellationToken);
        if (delivery == null) return NotFound();

        return Ok(delivery.Waypoints.Select(DeliveryResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Register a waypoint for a delivery", OperationId = "RegisterWaypoint")]
    public async Task<IActionResult> RegisterWaypoint([FromBody] RegisterWaypointResource resource, CancellationToken cancellationToken)
    {
        var command = RegisterWaypointCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await deliveryCommandService.Handle(command, cancellationToken);

        return DeliveryActionResultAssembler.ToActionResult(result, problemDetailsFactory,
            waypoint => Ok(DeliveryResourceFromEntityAssembler.ToResourceFromEntity(waypoint)));
    }

    [HttpPatch("{id:int}")]
    [SwaggerOperation(Summary = "Mark a waypoint as reached", OperationId = "MarkWaypointReached")]
    public async Task<IActionResult> MarkWaypointReached([FromRoute] int id, [FromBody] MarkWaypointReachedResource resource,
        CancellationToken cancellationToken)
    {
        var result = await deliveryCommandService.Handle(new MarkWaypointReachedCommand(resource.DeliveryId, id), cancellationToken);

        return DeliveryActionResultAssembler.ToActionResult(result, problemDetailsFactory,
            waypoint => Ok(DeliveryResourceFromEntityAssembler.ToResourceFromEntity(waypoint)));
    }
}
