using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Iam.Application.QueryServices;
using Flowbit.Qullqa.Platform.Iam.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Iam.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/roles")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Fixed role catalog (ADMIN, CASHIER, WAREHOUSE)")]
public class RolesController(IRoleQueryService roleQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "List all roles", OperationId = "GetAllRoles")]
    public async Task<IActionResult> GetAllRoles(CancellationToken cancellationToken)
    {
        var roles = await roleQueryService.Handle(new GetAllRolesQuery(), cancellationToken);
        return Ok(roles.Select(RoleResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
