using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Flowbit.Qullqa.Platform.Subscription.Application.CommandServices;
using Flowbit.Qullqa.Platform.Subscription.Application.QueryServices;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Subscription.Interfaces.Rest.Resources;
using Flowbit.Qullqa.Platform.Subscription.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Subscription.Interfaces.Rest;

/// <summary>
///     The plan catalog — read-mostly today (POST is admin/seed only, the
///     frontend has no plan-creation UI). Changing a business's plan is a
///     PATCH on IAM's /businesses/{id} (Business.PlanId), not a Subscription
///     endpoint — Stripe/checkout stays out of scope (see architecture doc §15).
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/plans")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Subscription plan catalog")]
public class PlansController(
    IPlanCommandService planCommandService,
    IPlanQueryService planQueryService,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "List the plan catalog (Free/Pro/Premium)", OperationId = "GetPlans")]
    public async Task<IActionResult> GetPlans(CancellationToken cancellationToken)
    {
        var plans = await planQueryService.Handle(new GetAllPlansQuery(), cancellationToken);
        return Ok(plans.Select(PlanResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get a plan by id", OperationId = "GetPlanById")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The plan was not found")]
    public async Task<IActionResult> GetPlanById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var plan = await planQueryService.Handle(new GetPlanByIdQuery(id), cancellationToken);
        return plan == null ? NotFound() : Ok(PlanResourceFromEntityAssembler.ToResourceFromEntity(plan));
    }

    /// <summary>Admin/seed only — no plan-creation UI exists in the frontend.</summary>
    [HttpPost]
    [SwaggerOperation(Summary = "Create a plan (admin/seed)", OperationId = "CreatePlan")]
    public async Task<IActionResult> CreatePlan([FromBody] CreatePlanResource resource, CancellationToken cancellationToken)
    {
        var command = CreatePlanCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await planCommandService.Handle(command, cancellationToken);

        return SubscriptionActionResultAssembler.ToActionResult(result, problemDetailsFactory,
            plan => CreatedAtAction(nameof(GetPlanById), new { id = plan.Id }, PlanResourceFromEntityAssembler.ToResourceFromEntity(plan)));
    }
}
