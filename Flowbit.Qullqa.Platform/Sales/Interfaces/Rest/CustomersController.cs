using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Qullqa.Platform.v2.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Qullqa.Platform.v2.Sales.Application.CommandServices;
using Qullqa.Platform.v2.Sales.Application.QueryServices;
using Qullqa.Platform.v2.Sales.Domain.Model.Commands;
using Qullqa.Platform.v2.Sales.Domain.Model.Queries;
using Qullqa.Platform.v2.Sales.Interfaces.Rest.Resources;
using Qullqa.Platform.v2.Sales.Interfaces.Rest.Transform;
using Qullqa.Platform.v2.Shared.Application;
using Qullqa.Platform.v2.Shared.Interfaces.Rest.ProblemDetails;
using Swashbuckle.AspNetCore.Annotations;

namespace Qullqa.Platform.v2.Sales.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/customers")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Customers of a business")]
public class CustomersController(
    ICustomerCommandService customerCommandService,
    ICustomerQueryService customerQueryService,
    ICurrentUserAccessor currentUserAccessor,
    ProblemDetailsFactory problemDetailsFactory)
    : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "List customers of the current business", OperationId = "GetCustomers")]
    public async Task<IActionResult> GetCustomers(CancellationToken cancellationToken)
    {
        var businessId = currentUserAccessor.CurrentBusinessId;
        if (businessId == null) return Unauthorized();

        var customers = await customerQueryService.Handle(new GetAllCustomersByBusinessIdQuery(businessId.Value), cancellationToken);
        return Ok(customers.Select(CustomerResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get a customer by id", OperationId = "GetCustomerById")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The customer was not found")]
    public async Task<IActionResult> GetCustomerById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var customer = await customerQueryService.Handle(new GetCustomerByIdQuery(id), cancellationToken);
        if (customer == null || customer.BusinessId != currentUserAccessor.CurrentBusinessId) return NotFound();

        return Ok(CustomerResourceFromEntityAssembler.ToResourceFromEntity(customer));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create a customer", OperationId = "CreateCustomer")]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerResource resource, CancellationToken cancellationToken)
    {
        var businessId = currentUserAccessor.CurrentBusinessId;
        if (businessId == null) return Unauthorized();

        var command = CreateCustomerCommandFromResourceAssembler.ToCommandFromResource(resource, businessId.Value);
        var result = await customerCommandService.Handle(command, cancellationToken);

        return SalesActionResultAssembler.ToActionResult(result, problemDetailsFactory,
            customer => CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id },
                CustomerResourceFromEntityAssembler.ToResourceFromEntity(customer)));
    }

    [HttpPatch("{id:int}")]
    [SwaggerOperation(Summary = "Update a customer", OperationId = "UpdateCustomer")]
    public async Task<IActionResult> UpdateCustomer([FromRoute] int id, [FromBody] UpdateCustomerResource resource,
        CancellationToken cancellationToken)
    {
        var command = UpdateCustomerCommandFromResourceAssembler.ToCommandFromResource(resource, id);
        var result = await customerCommandService.Handle(command, cancellationToken);

        return SalesActionResultAssembler.ToActionResult(result, problemDetailsFactory,
            customer => Ok(CustomerResourceFromEntityAssembler.ToResourceFromEntity(customer)));
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation(Summary = "Delete a customer", OperationId = "DeleteCustomer")]
    public async Task<IActionResult> DeleteCustomer([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await customerCommandService.Handle(new DeleteCustomerCommand(id), cancellationToken);
        return SalesActionResultAssembler.ToActionResult(result, problemDetailsFactory, () => NoContent());
    }
}
