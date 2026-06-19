using Microsoft.AspNetCore.Mvc;
using Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Qullqa.Platform.Sales.Application.CommandServices;
using Qullqa.Platform.Sales.Application.QueryServices;
using Qullqa.Platform.Sales.Domain.Model.Commands;
using Qullqa.Platform.Sales.Domain.Model.Queries;
using Qullqa.Platform.Sales.Interfaces.Rest.Resources;
using Qullqa.Platform.Sales.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Qullqa.Platform.Sales.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Customer management")]
public class CustomersController(ICustomerCommandService customerCommandService, ICustomerQueryService customerQueryService) : ControllerBase
{
    [HttpGet("business/{businessId:int}")]
    [SwaggerOperation("Get all customers by business")]
    public async Task<IActionResult> GetByBusiness(int businessId, CancellationToken cancellationToken)
    {
        var customers = await customerQueryService.Handle(new GetCustomersByBusinessQuery(businessId), cancellationToken);
        return Ok(customers.Select(CustomerResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost]
    [SwaggerOperation("Create customer")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerResource resource, CancellationToken cancellationToken)
    {
        var result = await customerCommandService.Handle(new CreateCustomerCommand(resource.BusinessId, resource.FullName, resource.DocumentNumber, resource.PhoneNumber), cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Created(string.Empty, CustomerResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
