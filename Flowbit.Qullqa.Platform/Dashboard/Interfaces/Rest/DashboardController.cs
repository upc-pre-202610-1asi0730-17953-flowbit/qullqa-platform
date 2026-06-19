using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Attributes;
using Flowbit.Qullqa.Platform.Dashboard.Application.CommandServices;
using Flowbit.Qullqa.Platform.Dashboard.Application.QueryServices;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Commands;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Queries;
using Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Resources;
using Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Dashboard and reporting")]
public class DashboardController(IDashboardCommandService dashboardCommandService, IDashboardQueryService dashboardQueryService) : ControllerBase
{
    [HttpGet("business/{businessId:int}/metrics")]
    [SwaggerOperation("Get latest metrics for a business")]
    public async Task<IActionResult> GetMetrics(int businessId, CancellationToken cancellationToken)
    {
        var snapshot = await dashboardQueryService.Handle(new GetMetricsByBusinessQuery(businessId), cancellationToken);
        return snapshot is null ? NotFound() : Ok(MetricsSnapshotResourceFromEntityAssembler.ToResourceFromEntity(snapshot));
    }

    [HttpGet("business/{businessId:int}/reports")]
    [SwaggerOperation("Get reports for a business")]
    public async Task<IActionResult> GetReports(int businessId, CancellationToken cancellationToken)
    {
        var reports = await dashboardQueryService.Handle(new GetReportsByBusinessQuery(businessId), cancellationToken);
        return Ok(reports.Select(ReportResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpPost("reports")]
    [SwaggerOperation("Generate a report")]
    public async Task<IActionResult> GenerateReport([FromBody] GenerateReportResource resource, CancellationToken cancellationToken)
    {
        var result = await dashboardCommandService.Handle(
            new GenerateReportCommand(resource.BusinessId, resource.Type, resource.Filters), cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Created(string.Empty, ReportResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }

    [HttpPost("metrics")]
    [SwaggerOperation("Record a metrics snapshot")]
    public async Task<IActionResult> RecordMetrics([FromBody] RecordMetricsResource resource, CancellationToken cancellationToken)
    {
        var result = await dashboardCommandService.Handle(
            new RecordMetricsSnapshotCommand(resource.BusinessId, resource.TotalProducts, resource.TotalSales, resource.TotalRevenue, resource.LowStockCount),
            cancellationToken);
        return result.IsFailure ? BadRequest(result.Message) : Created(string.Empty, MetricsSnapshotResourceFromEntityAssembler.ToResourceFromEntity(result.Value!));
    }
}
