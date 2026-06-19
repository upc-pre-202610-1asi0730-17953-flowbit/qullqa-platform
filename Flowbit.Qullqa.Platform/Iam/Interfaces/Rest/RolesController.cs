using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Iam.Application.CommandServices;
using Flowbit.Qullqa.Platform.Iam.Application.QueryServices;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Resources;
using Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Role endpoints")]
public class RolesController(IRoleQueryService roleQueryService, IRoleCommandService roleCommandService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all roles", OperationId = "GetAllRoles")]
    [SwaggerResponse(StatusCodes.Status200OK, "Roles found", typeof(IEnumerable<RoleResource>))]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var roles = await roleQueryService.Handle(new GetAllRolesQuery(), cancellationToken);
        return Ok(roles.Select(RoleResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create role", OperationId = "CreateRole")]
    [SwaggerResponse(StatusCodes.Status201Created, "Role created", typeof(RoleResource))]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleResource resource, CancellationToken cancellationToken)
    {
        var command = new CreateRoleCommand(resource.Name, resource.Description);
        var result = await roleCommandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(new { message = result.Message });
        return StatusCode(StatusCodes.Status201Created, RoleResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
