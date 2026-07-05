using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Alerts.Application.CommandServices;
using Flowbit.Qullqa.Platform.Alerts.Application.QueryServices;
using Flowbit.Qullqa.Platform.Alerts.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Alerts.Interfaces.Rest.Resources;
using Flowbit.Qullqa.Platform.Alerts.Interfaces.Rest.Transform;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Shared.Application;
using Flowbit.Qullqa.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Alerts.Interfaces.Rest;

/// <summary>Replaces the frontend's decorative "Reglas" tab with a real, configurable-per-business resource (see architecture doc §5.4).</summary>
[Authorize]
[ApiController]
[Route("api/v1/alert-rules")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Configurable alert thresholds per business")]
public class AlertRulesController(
    IAlertRuleCommandService alertRuleCommandService,
    IAlertRuleQueryService alertRuleQueryService,
    ICurrentUserAccessor currentUserAccessor,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "List alert rules of the current business", OperationId = "GetAlertRules")]
    public async Task<IActionResult> GetAlertRules(CancellationToken cancellationToken)
    {
        var businessId = currentUserAccessor.CurrentBusinessId;
        if (businessId == null) return Unauthorized();

        var rules = await alertRuleQueryService.Handle(new GetAlertRulesByBusinessIdQuery(businessId.Value), cancellationToken);
        return Ok(rules.Select(AlertRuleResourceFromEntityAssembler.ToResourceFromEntity));
    }

    /// <summary>Creates or updates the rule for the given AlertType — one rule per (business, type).</summary>
    [HttpPost]
    [SwaggerOperation(Summary = "Create or update an alert rule", OperationId = "CreateOrUpdateAlertRule")]
    public async Task<IActionResult> CreateOrUpdateAlertRule([FromBody] CreateOrUpdateAlertRuleResource resource,
        CancellationToken cancellationToken)
    {
        var businessId = currentUserAccessor.CurrentBusinessId;
        if (businessId == null) return Unauthorized();

        var command = CreateOrUpdateAlertRuleCommandFromResourceAssembler.ToCommandFromResource(resource, businessId.Value);
        var result = await alertRuleCommandService.Handle(command, cancellationToken);

        return AlertsActionResultAssembler.ToActionResult(result, problemDetailsFactory,
            rule => Ok(AlertRuleResourceFromEntityAssembler.ToResourceFromEntity(rule)));
    }

    [HttpPatch]
    [SwaggerOperation(Summary = "Update an alert rule (alias for POST — same upsert semantics)", OperationId = "UpdateAlertRule")]
    public Task<IActionResult> UpdateAlertRule([FromBody] CreateOrUpdateAlertRuleResource resource, CancellationToken cancellationToken)
    {
        return CreateOrUpdateAlertRule(resource, cancellationToken);
    }
}
