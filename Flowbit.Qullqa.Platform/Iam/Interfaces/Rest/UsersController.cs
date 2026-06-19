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
[SwaggerTag("User endpoints")]
public class UsersController(IUserQueryService userQueryService, IUserCommandService userCommandService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all users", OperationId = "GetAllUsers")]
    [SwaggerResponse(StatusCodes.Status200OK, "Users found", typeof(IEnumerable<UserResource>))]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var users = await userQueryService.Handle(new GetAllUsersQuery(), cancellationToken);
        return Ok(users.Select(UserResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get user by id", OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "User found", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
    {
        var user = await userQueryService.Handle(new GetUserByIdQuery(id), cancellationToken);
        if (user == null) return NotFound(new { message = $"User with id {id} not found." });
        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(user));
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update user", OperationId = "UpdateUser")]
    [SwaggerResponse(StatusCodes.Status200OK, "User updated", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserResource resource, CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand(id, resource.FirstName, resource.LastName, resource.Status, resource.RoleId, resource.BusinessId);
        var result = await userCommandService.Handle(command, cancellationToken);
        if (result.IsFailure) return NotFound(new { message = result.Message });
        return Ok(UserResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
