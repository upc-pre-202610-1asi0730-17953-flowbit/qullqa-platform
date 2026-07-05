using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Qullqa.Platform.v2.Shared.Application.Model;
using Qullqa.Platform.v2.Shared.Interfaces.Rest.ProblemDetails;
using Qullqa.Platform.v2.Subscription.Domain.Model.Errors;

namespace Qullqa.Platform.v2.Subscription.Interfaces.Rest.Transform;

public static class SubscriptionActionResultAssembler
{
    public static IActionResult ToActionResult<T>(
        Result<T> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> onSuccess)
    {
        if (result.IsSuccess) return onSuccess(result.Value!);

        var statusCode = result.Error is SubscriptionError.PlanNotFound
            ? StatusCodes.Status404NotFound
            : StatusCodes.Status500InternalServerError;
        return problemDetailsFactory.ToActionResult(statusCode, result.Error?.ToString() ?? "InternalServerError", result.Message);
    }
}
