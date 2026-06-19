using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Suppliers.Application.CommandServices;
using Flowbit.Qullqa.Platform.Suppliers.Application.QueryServices;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Suppliers.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;
using Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Suppliers.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Supplier management")]
public class SuppliersController(ISupplierCommandService supplierCommandService, ISupplierQueryService supplierQueryService) : ControllerBase
{
    [HttpGet("business/{businessId:int}")]
    [SwaggerOperation("Get suppliers by business")]
    public async Task<IActionResult> GetByBusiness(int businessId, CancellationToken cancellationToken)
    {
        var suppliers = await supplierQueryService.Handle(new GetSuppliersByBusinessQuery(businessId), cancellationToken);
        return Ok(suppliers.Select(SupplierResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get supplier by id")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var supplier = await supplierQueryService.Handle(new GetSupplierByIdQuery(id), cancellationToken);
        return supplier is null ? NotFound() : Ok(SupplierResourceFromEntityAssembler.ToResourceFromEntity(supplier));
    }

    [HttpPost]
    [SwaggerOperation("Create supplier")]
    public async Task<IActionResult> Create([FromBody] CreateSupplierResource resource, CancellationToken cancellationToken)
    {
        var result = await supplierCommandService.Handle(
            new CreateSupplierCommand(resource.BusinessId, resource.Name, resource.LastName, resource.Ruc,
                resource.Email, resource.Phone, resource.Address, resource.ContactPerson, resource.Category),
            cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, SupplierResourceFromEntityAssembler.ToResourceFromEntity(result.Value));
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation("Update supplier")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateSupplierResource resource, CancellationToken cancellationToken)
    {
        var result = await supplierCommandService.Handle(
            new UpdateSupplierCommand(id, resource.Name, resource.LastName, resource.Email, resource.Phone,
                resource.Address, resource.ContactPerson, resource.Category),
            cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Ok(SupplierResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
