using Ecommerce.Api.Middleware;

namespace Ecommerce.Api.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptions(this IApplicationBuilder app) 
        { 
            return app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
