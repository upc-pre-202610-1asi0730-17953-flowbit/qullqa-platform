using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Dashboard.Domain.Model.Errors;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace Flowbit.Qullqa.Platform.Dashboard.Interfaces.Rest.Transform;

public static class DashboardActionResultAssembler
{
    public static IActionResult ToActionResult<T>(
        Result<T> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);

        var statusCode = result.Error is DashboardError.ReportNotFound
            ? StatusCodes.Status404NotFound
            : StatusCodes.Status500InternalServerError;
        return problemDetailsFactory.ToActionResult(statusCode, result.Error?.ToString() ?? "InternalServerError", result.Message);
    }
}
