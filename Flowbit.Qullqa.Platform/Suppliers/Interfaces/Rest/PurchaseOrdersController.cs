using Microsoft.AspNetCore.Mvc;
using Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Qullqa.Platform.Suppliers.Application.CommandServices;
using Qullqa.Platform.Suppliers.Application.QueryServices;
using Qullqa.Platform.Suppliers.Domain.Model.Commands;
using Qullqa.Platform.Suppliers.Domain.Model.Queries;
using Qullqa.Platform.Suppliers.Interfaces.Rest.Resources;
using Qullqa.Platform.Suppliers.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Qullqa.Platform.Suppliers.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/purchase-orders")]
[SwaggerTag("Purchase order management")]
public class PurchaseOrdersController(IPurchaseOrderCommandService purchaseOrderCommandService, IPurchaseOrderQueryService purchaseOrderQueryService) : ControllerBase
{
    [HttpGet("business/{businessId:int}")]
    [SwaggerOperation("Get purchase orders by business")]
    public async Task<IActionResult> GetByBusiness(int businessId, CancellationToken cancellationToken)
    {
        var orders = await purchaseOrderQueryService.Handle(new GetPurchaseOrdersByBusinessQuery(businessId), cancellationToken);
        return Ok(orders.Select(PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get purchase order by id")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var order = await purchaseOrderQueryService.Handle(new GetPurchaseOrderByIdQuery(id), cancellationToken);
        return order is null ? NotFound() : Ok(PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(order));
    }

    [HttpPost]
    [SwaggerOperation("Create purchase order")]
    public async Task<IActionResult> Create([FromBody] CreatePurchaseOrderResource resource, CancellationToken cancellationToken)
    {
        var result = await purchaseOrderCommandService.Handle(
            new CreatePurchaseOrderCommand(resource.BusinessId, resource.SupplierId, resource.SupplierName,
                resource.ExpectedDate, resource.Description, resource.Currency),
            cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(result.Value));
    }

    [HttpPost("{id:int}/details")]
    [SwaggerOperation("Add detail to purchase order")]
    public async Task<IActionResult> AddDetail(int id, [FromBody] AddPurchaseOrderDetailResource resource, CancellationToken cancellationToken)
    {
        var result = await purchaseOrderCommandService.Handle(
            new AddPurchaseOrderDetailCommand(id, resource.ProductId, resource.ProductName, resource.Quantity, resource.UnitPrice, resource.Discount),
            cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Ok(PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpPut("{id:int}/status")]
    [SwaggerOperation("Update purchase order status")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdatePurchaseOrderStatusResource resource, CancellationToken cancellationToken)
    {
        var result = await purchaseOrderCommandService.Handle(new UpdatePurchaseOrderStatusCommand(id, resource.Status), cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Ok(PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
