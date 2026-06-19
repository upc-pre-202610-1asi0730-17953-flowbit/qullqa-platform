using Microsoft.AspNetCore.Mvc;
using Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Qullqa.Platform.Alerts.Application.CommandServices;
using Qullqa.Platform.Alerts.Application.QueryServices;
using Qullqa.Platform.Alerts.Domain.Model.Commands;
using Qullqa.Platform.Alerts.Domain.Model.Queries;
using Qullqa.Platform.Alerts.Interfaces.Rest.Resources;
using Qullqa.Platform.Alerts.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Qullqa.Platform.Alerts.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Alert management")]
public class AlertsController(IAlertCommandService alertCommandService, IAlertQueryService alertQueryService) : ControllerBase
{
    [HttpGet("business/{businessId:int}")]
    [SwaggerOperation("Get alerts by business")]
    public async Task<IActionResult> GetByBusiness(int businessId, CancellationToken cancellationToken)
    {
        var alerts = await alertQueryService.Handle(new GetAlertsByBusinessQuery(businessId), cancellationToken);
        return Ok(alerts.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get alert by id")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var alert = await alertQueryService.Handle(new GetAlertByIdQuery(id), cancellationToken);
        return alert is null ? NotFound() : Ok(AlertResourceFromEntityAssembler.ToResourceFromEntity(alert));
    }

    [HttpPost]
    [SwaggerOperation("Create alert")]
    public async Task<IActionResult> Create([FromBody] CreateAlertResource resource, CancellationToken cancellationToken)
    {
        var result = await alertCommandService.Handle(
            new CreateAlertCommand(resource.BusinessId, resource.ProductId, resource.ProductName, resource.Type,
                resource.Severity, resource.Message, resource.BatchId, resource.CurrentStock, resource.MinStock, resource.DaysToExpiry),
            cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, AlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value));
    }

    [HttpPut("{id:int}/status")]
    [SwaggerOperation("Update alert status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateAlertStatusResource resource, CancellationToken cancellationToken)
    {
        var result = await alertCommandService.Handle(new UpdateAlertStatusCommand(id, resource.Status), cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Ok(AlertResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
