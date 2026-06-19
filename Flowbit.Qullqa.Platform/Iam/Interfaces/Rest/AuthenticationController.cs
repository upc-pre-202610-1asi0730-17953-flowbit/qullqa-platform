using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Qullqa.Platform.Iam.Application.CommandServices;
using Qullqa.Platform.Iam.Domain.Model.Commands;
using Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Qullqa.Platform.Iam.Interfaces.Rest.Resources;
using Qullqa.Platform.Iam.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Qullqa.Platform.Iam.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Authentication endpoints")]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Sign in", OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "Authenticated", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid credentials")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource, CancellationToken cancellationToken)
    {
        var command = new SignInCommand(resource.Email, resource.Password);
        var result = await userCommandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(new { message = result.Message });
        return Ok(AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(result.Value!.user, result.Value!.token));
    }

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(Summary = "Sign up", OperationId = "SignUp")]
    [SwaggerResponse(StatusCodes.Status200OK, "User created")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Registration failed")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource, CancellationToken cancellationToken)
    {
        var command = new SignUpCommand(resource.Email, resource.Password, resource.FirstName, resource.LastName);
        var result = await userCommandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(new { message = result.Message });
        return Ok(new { message = "User created successfully." });
    }
}
