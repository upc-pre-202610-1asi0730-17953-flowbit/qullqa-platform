using Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Components;

namespace Flowbit.Qullqa.Platform.Iam.Infrastructure.Pipeline.Middleware.Extensions;

public static class RequestAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestAuthorization(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}
