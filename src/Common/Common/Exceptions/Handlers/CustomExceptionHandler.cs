using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace Common.Exceptions.Handlers
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error message: {exceptionMessage}, Occurence Time: {time}", exception.Message, DateTime.Now);

            (string Detail, string Title, int StatusCode) details = exception switch
            {
                InternalServerException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    StatusCodes.Status500InternalServerError
                ),
                ValidationException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    StatusCodes.Status400BadRequest
                ),
                NotFoundException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    StatusCodes.Status404NotFound
                ),
                BadRequestException =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    StatusCodes.Status400BadRequest
                ),
                _ =>
                (
                    exception.Message,
                    exception.GetType().Name,
                    StatusCodes.Status500InternalServerError
                )
            };

            var problemDetails = new ProblemDetails()
            {
                Title = details.Title,
                Status = details.StatusCode,
                Detail = details.Detail,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            if (exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("validationErrors", validationException.Message);
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);
            return true;
        }
    }
}
