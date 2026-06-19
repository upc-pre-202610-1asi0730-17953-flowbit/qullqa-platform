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
[SwaggerTag("Business endpoints")]
public class BusinessesController(IBusinessQueryService businessQueryService, IBusinessCommandService businessCommandService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get all businesses", OperationId = "GetAllBusinesses")]
    [SwaggerResponse(StatusCodes.Status200OK, "Businesses found", typeof(IEnumerable<BusinessResource>))]
    public async Task<IActionResult> GetAllBusinesses(CancellationToken cancellationToken)
    {
        var businesses = await businessQueryService.Handle(new GetAllBusinessesQuery(), cancellationToken);
        return Ok(businesses.Select(BusinessResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get business by id", OperationId = "GetBusinessById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Business found", typeof(BusinessResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Business not found")]
    public async Task<IActionResult> GetBusinessById(int id, CancellationToken cancellationToken)
    {
        var business = await businessQueryService.Handle(new GetBusinessByIdQuery(id), cancellationToken);
        if (business == null) return NotFound(new { message = $"Business with id {id} not found." });
        return Ok(BusinessResourceFromEntityAssembler.ToResourceFromEntity(business));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create business", OperationId = "CreateBusiness")]
    [SwaggerResponse(StatusCodes.Status201Created, "Business created", typeof(BusinessResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    public async Task<IActionResult> CreateBusiness([FromBody] CreateBusinessResource resource, CancellationToken cancellationToken)
    {
        var command = new CreateBusinessCommand(resource.Name, resource.Ruc, resource.Email, resource.Phone, resource.Address);
        var result = await businessCommandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(new { message = result.Message });
        var businessResource = BusinessResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetBusinessById), new { id = result.Value!.Id }, businessResource);
    }
}
