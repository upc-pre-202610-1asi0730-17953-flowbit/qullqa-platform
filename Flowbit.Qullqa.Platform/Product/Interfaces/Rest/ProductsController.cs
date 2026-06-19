using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Product.Application.CommandServices;
using Flowbit.Qullqa.Platform.Product.Application.QueryServices;
using Flowbit.Qullqa.Platform.Product.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Product.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Product.Interfaces.Rest.Resources;
using Flowbit.Qullqa.Platform.Product.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Product.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Product & Inventory endpoints")]
public class ProductsController(IProductQueryService productQueryService, IProductCommandService productCommandService) : ControllerBase
{
    [HttpGet("business/{businessId:int}")]
    [SwaggerOperation(Summary = "Get all products by business", OperationId = "GetAllProductsByBusiness")]
    [SwaggerResponse(StatusCodes.Status200OK, "Products found", typeof(IEnumerable<ProductResource>))]
    public async Task<IActionResult> GetAllProductsByBusiness(int businessId, CancellationToken cancellationToken)
    {
        var products = await productQueryService.Handle(new GetAllProductsByBusinessQuery(businessId), cancellationToken);
        return Ok(products.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{id:int}")]
    [SwaggerOperation(Summary = "Get product by id", OperationId = "GetProductById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Product found", typeof(ProductResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found")]
    public async Task<IActionResult> GetProductById(int id, CancellationToken cancellationToken)
    {
        var product = await productQueryService.Handle(new GetProductByIdQuery(id), cancellationToken);
        if (product == null) return NotFound(new { message = $"Product with id {id} not found." });
        return Ok(ProductResourceFromEntityAssembler.ToResourceFromEntity(product));
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create product", OperationId = "CreateProduct")]
    [SwaggerResponse(StatusCodes.Status201Created, "Product created", typeof(ProductResource))]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductResource resource, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand(resource.BusinessId, resource.Name, resource.Description, resource.Category, resource.BasePrice);
        var result = await productCommandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(new { message = result.Message });
        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(result.Value!);
        return CreatedAtAction(nameof(GetProductById), new { id = result.Value!.Id }, productResource);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation(Summary = "Update product", OperationId = "UpdateProduct")]
    [SwaggerResponse(StatusCodes.Status200OK, "Product updated", typeof(ProductResource))]
    public async Task<IActionResult> UpdateProduct(int id, [FromBody] UpdateProductResource resource, CancellationToken cancellationToken)
    {
        var command = new UpdateProductCommand(id, resource.Name, resource.Description, resource.Category, resource.BasePrice, resource.Status);
        var result = await productCommandService.Handle(command, cancellationToken);
        if (result.IsFailure) return NotFound(new { message = result.Message });
        return Ok(ProductResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpGet("business/{businessId:int}/inventory")]
    [SwaggerOperation(Summary = "Get inventory by business", OperationId = "GetInventoryByBusiness")]
    [SwaggerResponse(StatusCodes.Status200OK, "Inventory found", typeof(IEnumerable<InventoryItemResource>))]
    public async Task<IActionResult> GetInventoryByBusiness(int businessId, CancellationToken cancellationToken)
    {
        var items = await productQueryService.Handle(new GetInventoryByBusinessQuery(businessId), cancellationToken);
        return Ok(items.Select(InventoryItemResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost("stock-movements")]
    [SwaggerOperation(Summary = "Register stock movement", OperationId = "RegisterStockMovement")]
    [SwaggerResponse(StatusCodes.Status201Created, "Movement registered", typeof(StockMovementResource))]
    public async Task<IActionResult> RegisterStockMovement([FromBody] RegisterStockMovementResource resource, CancellationToken cancellationToken)
    {
        var command = new RegisterStockMovementCommand(resource.ProductId, resource.BusinessId, resource.Quantity, resource.Type);
        var result = await productCommandService.Handle(command, cancellationToken);
        if (result.IsFailure) return BadRequest(new { message = result.Message });
        return StatusCode(StatusCodes.Status201Created, StockMovementResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpGet("{productId:int}/stock-movements/business/{businessId:int}")]
    [SwaggerOperation(Summary = "Get stock movements by product", OperationId = "GetStockMovementsByProduct")]
    [SwaggerResponse(StatusCodes.Status200OK, "Movements found", typeof(IEnumerable<StockMovementResource>))]
    public async Task<IActionResult> GetStockMovementsByProduct(int productId, int businessId, CancellationToken cancellationToken)
    {
        var movements = await productQueryService.Handle(new GetStockMovementsByProductQuery(productId, businessId), cancellationToken);
        return Ok(movements.Select(StockMovementResourceFromEntityAssembler.ToResourceFromEntity));
    }
}
