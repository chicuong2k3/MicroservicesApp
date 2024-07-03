

using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Common.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse>(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
        where TResponse : notnull
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("[START] Request={Request} - Response={Response} - RequestData={RequestData}", typeof(TRequest).Name, typeof(TResponse).Name, request);
            
            var timer = new Stopwatch();
            timer.Start();
            var response = await next();

            timer.Stop();
            var processTime = timer.Elapsed;
            logger.LogInformation("[PERFORMANCE] The time to process request {Request} is {ProcessTime}", typeof(TRequest).Name, processTime.Seconds);

            logger.LogInformation("[END] The response is {Response}", typeof(TResponse).Name);

            return response;
        }
    }
}
