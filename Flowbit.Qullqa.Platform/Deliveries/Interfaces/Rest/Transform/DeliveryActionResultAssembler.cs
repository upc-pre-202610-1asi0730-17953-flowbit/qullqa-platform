using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Flowbit.Qullqa.Platform.Deliveries.Domain.Model.Errors;
using Flowbit.Qullqa.Platform.Shared.Application.Model;
using Flowbit.Qullqa.Platform.Shared.Interfaces.Rest.ProblemDetails;

namespace Flowbit.Qullqa.Platform.Deliveries.Interfaces.Rest.Transform;

public static class DeliveryActionResultAssembler
{
    public static IActionResult ToActionResult<T>(
        Result<T> result,
        ProblemDetailsFactory problemDetailsFactory,
        Func<T, IActionResult> onSuccess)
    {
        return result.IsSuccess
            ? onSuccess(result.Value!)
            : ToProblemResult(result.Error, result.Message, problemDetailsFactory);
    }

    private static IActionResult ToProblemResult(Enum? error, string message, ProblemDetailsFactory problemDetailsFactory)
    {
        var statusCode = error is DeliveryError
            ? StatusCodes.Status404NotFound
            : StatusCodes.Status500InternalServerError;
        return problemDetailsFactory.ToActionResult(statusCode, error?.ToString() ?? "InternalServerError", message);
    }
}
