using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Sales.Application.CommandServices;
using Flowbit.Qullqa.Platform.Sales.Application.QueryServices;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Sales.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Resources;
using Flowbit.Qullqa.Platform.Sales.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Sales.Interfaces.Rest;

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
