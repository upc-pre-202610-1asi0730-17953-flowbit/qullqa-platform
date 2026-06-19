using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Delivery.Application.CommandServices;
using Flowbit.Qullqa.Platform.Delivery.Application.QueryServices;
using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Delivery.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Delivery.Interfaces.Rest.Resources;
using Flowbit.Qullqa.Platform.Delivery.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Delivery.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Delivery management")]
public class DeliveriesController(IDeliveryCommandService deliveryCommandService, IDeliveryQueryService deliveryQueryService) : ControllerBase
{
    [HttpGet("business/{businessId:int}")]
    [SwaggerOperation("Get deliveries by business")]
    public async Task<IActionResult> GetByBusiness(int businessId, CancellationToken cancellationToken)
    {
        var deliveries = await deliveryQueryService.Handle(new GetDeliveriesByBusinessQuery(businessId), cancellationToken);
        return Ok(deliveries.Select(DeliveryResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get delivery by id")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var delivery = await deliveryQueryService.Handle(new GetDeliveryByIdQuery(id), cancellationToken);
        return delivery is null ? NotFound() : Ok(DeliveryResourceFromEntityAssembler.ToResourceFromEntity(delivery));
    }

    [HttpGet("tracking/{trackingNumber}")]
    [SwaggerOperation("Get delivery by tracking number")]
    public async Task<IActionResult> GetByTracking(string trackingNumber, CancellationToken cancellationToken)
    {
        var delivery = await deliveryQueryService.Handle(new GetDeliveryByTrackingNumberQuery(trackingNumber), cancellationToken);
        return delivery is null ? NotFound() : Ok(DeliveryResourceFromEntityAssembler.ToResourceFromEntity(delivery));
    }

    [HttpPost]
    [SwaggerOperation("Create delivery")]
    public async Task<IActionResult> Create([FromBody] CreateDeliveryResource resource, CancellationToken cancellationToken)
    {
        var result = await deliveryCommandService.Handle(
            new CreateDeliveryCommand(resource.BusinessId, resource.SupplierName, resource.Origin, resource.Destination,
                resource.DriverName, resource.DriverPhone, resource.Vehicle, resource.LicensePlate,
                resource.EstimatedArrival, resource.TotalWeight, resource.PurchaseDetailId),
            cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, DeliveryResourceFromEntityAssembler.ToResourceFromEntity(result.Value));
    }

    [HttpPut("{id:int}/status")]
    [SwaggerOperation("Update delivery status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateDeliveryStatusResource resource, CancellationToken cancellationToken)
    {
        var result = await deliveryCommandService.Handle(new UpdateDeliveryStatusCommand(id, resource.Status), cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Ok(DeliveryResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpPut("{id:int}/location")]
    [SwaggerOperation("Update delivery location")]
    public async Task<IActionResult> UpdateLocation(int id, [FromBody] UpdateDeliveryLocationResource resource, CancellationToken cancellationToken)
    {
        var result = await deliveryCommandService.Handle(new UpdateDeliveryLocationCommand(id, resource.Label, resource.Latitude, resource.Longitude), cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Ok(DeliveryResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpPost("{id:int}/waypoints")]
    [SwaggerOperation("Add waypoint to delivery")]
    public async Task<IActionResult> AddWaypoint(int id, [FromBody] AddWaypointResource resource, CancellationToken cancellationToken)
    {
        var result = await deliveryCommandService.Handle(new AddWaypointCommand(id, resource.Label, resource.Latitude, resource.Longitude, resource.SequenceOrder), cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Ok(DeliveryResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
