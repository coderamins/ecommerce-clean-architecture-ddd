using Ecommerce.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Ecommerce.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        private readonly IRequestContext _requestContext;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger, 
            IRequestContext requestContext)
        {
            _logger = logger;
            _requestContext = requestContext;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            var requestName = typeof(TRequest).Name;

            try
            {
                _logger.LogInformation(
                    "Handling {requestName}. CorrelationId:{CorrelatonId}. Request: {@Request}",
                    requestName,
                    _requestContext.CorrelationId,
                    request
                    );

                var response = await next();

                stopwatch.Stop();

                _logger.LogInformation(
                    "Handling {RequestName} in {Elapsed} ms. CorrelatonId: {CorrelatonId}",
                    requestName,
                    stopwatch.ElapsedMilliseconds,
                    _requestContext.CorrelationId
                    );

                return response;
            }
            catch (Exception ex)
            {
                stopwatch.Stop();

                _logger.LogError(
                        ex,
                        "Handling {RequestName} after {Elapsed} ms. CorrelatonId: {CorrelatonId}",
                        requestName,
                        stopwatch.ElapsedMilliseconds,
                        _requestContext.CorrelationId
                    );

                throw;
            }
        }
    }
}
