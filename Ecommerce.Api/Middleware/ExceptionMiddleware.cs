using Ecommerce.Domain.Common;
using FluentValidation;
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
            catch (ValidationException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = ex.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).ToArray());

                await context.Response.WriteAsJsonAsync(new
                {
                    Title = "Validation failed",
                    Errors = errors
                });

                return;
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

