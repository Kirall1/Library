using Microsoft.AspNetCore.Mvc;
using Library.Business.Exceptions;

namespace Library.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(
                    exception, "Exception occurred: {Message}", exception.Message);

                var code = exception switch
                {
                    BadRequestException => StatusCodes.Status400BadRequest,
                    NotFoundException => StatusCodes.Status404NotFound,
                    ClientClosedRequest => StatusCodes.Status499ClientClosedRequest,
                    UnprocessableRequestException => StatusCodes.Status422UnprocessableEntity,
                    _ => StatusCodes.Status500InternalServerError
                };
                var problemDetails = new ProblemDetails
                {
                    Status = code,
                    Title = "Exception occurred",
                    Detail = exception.Message
                };

                context.Response.StatusCode = code;

                await context.Response.WriteAsJsonAsync(problemDetails);
            }
        }
    }
}
