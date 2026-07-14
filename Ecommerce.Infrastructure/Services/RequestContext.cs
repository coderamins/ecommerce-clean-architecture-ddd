using Ecommerce.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Infrastructure.Services
{
    public class RequestContext:IRequestContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string CorrelationId
        {
            get
            {
                var context=_httpContextAccessor.HttpContext;

                if (context is null)
                    return string.Empty;

                return context.Items["CorrelationId"]?.ToString() ?? string.Empty;  
            }    
        }

        public string? UserId
        {
            get
            {
                return _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirst("sub")?
                    .Value;
            }
        }

        public string? IpAddress
        {
            get
            {
                return _httpContextAccessor
                    .HttpContext?
                    .Connection
                    .RemoteIpAddress?.ToString();
            }
        }
    }
}
