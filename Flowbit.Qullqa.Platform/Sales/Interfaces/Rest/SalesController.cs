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
[SwaggerTag("Sales management")]
public class SalesController(ISaleCommandService saleCommandService, ISaleQueryService saleQueryService) : ControllerBase
{
    [HttpGet("business/{businessId:int}")]
    [SwaggerOperation("Get all sales by business")]
    public async Task<IActionResult> GetByBusiness(int businessId, CancellationToken cancellationToken)
    {
        var sales = await saleQueryService.Handle(new GetSalesByBusinessQuery(businessId), cancellationToken);
        return Ok(sales.Select(SaleResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation("Get sale by id")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    {
        var sale = await saleQueryService.Handle(new GetSaleByIdQuery(id), cancellationToken);
        return sale is null ? NotFound() : Ok(SaleResourceFromEntityAssembler.ToResourceFromEntity(sale));
    }

    [HttpPost]
    [SwaggerOperation("Create sale")]
    public async Task<IActionResult> Create([FromBody] CreateSaleResource resource, CancellationToken cancellationToken)
    {
        var result = await saleCommandService.Handle(CreateSaleCommandFromResourceAssembler.ToCommandFromResource(resource), cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : CreatedAtAction(nameof(GetById), new { id = result.Value!.Id }, SaleResourceFromEntityAssembler.ToResourceFromEntity(result.Value));
    }

    [HttpPost("{id:int}/details")]
    [SwaggerOperation("Add detail to sale")]
    public async Task<IActionResult> AddDetail(int id, [FromBody] AddSaleDetailResource resource, CancellationToken cancellationToken)
    {
        var result = await saleCommandService.Handle(new AddSaleDetailCommand(id, resource.ProductId, resource.Quantity, resource.UnitPrice, resource.Discount), cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Ok(SaleResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpPut("{id:int}/pay")]
    [SwaggerOperation("Pay a sale")]
    public async Task<IActionResult> Pay(int id, [FromBody] PaySaleResource resource, CancellationToken cancellationToken)
    {
        var result = await saleCommandService.Handle(new PaySaleCommand(id, resource.PaymentMethod, resource.TotalAmount), cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Ok(SaleResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpPut("{id:int}/cancel")]
    [SwaggerOperation("Cancel a sale")]
    public async Task<IActionResult> Cancel(int id, CancellationToken cancellationToken)
    {
        var result = await saleCommandService.Handle(new CancelSaleCommand(id), cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Ok(SaleResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
