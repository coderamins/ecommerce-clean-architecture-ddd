using Ecommerce.Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (DomainException ex)
            {
                await Handle(context, StatusCodes.Status400BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                await Handle(context, StatusCodes.Status500InternalServerError, "Unexpected error");
            }
        }

        private static async Task Handle(HttpContext context,
               int status,
               string detail)
        {
            context.Response.StatusCode = status;

            var problem = new ProblemDetails
            {
                Status = status,
                Title = "Request failed",
                Detail = detail
            };

            await context.Response.WriteAsJsonAsync(problem);
        }
    }
}

