using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Interfaces.Rest.ProblemDetails;
using Flowbit.Qullqa.Platform.Subscription.Domain.Model.Errors;

namespace Flowbit.Qullqa.Platform.Subscription.Interfaces.Rest.Transform;

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
