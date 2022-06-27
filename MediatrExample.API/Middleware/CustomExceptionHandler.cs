using MediatrExample.Shared.CustomExceptions;
using MediatrExample.Shared.DataModels;
using System.Text.Json;

namespace MediatrExample.API.Middleware
{
    internal sealed class CustomExceptionHandler : IMiddleware
    {
        private readonly ILogger<CustomExceptionHandler> _logger;
        public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) => _logger = logger;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception e)
            {
                if (!(e is MyHttpException || e is ValidationException))
                {
                    _logger.LogError(e, e.Message);
                }
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var response = new GenericResponse<NoData>
            {
                HttpCode = GetStatusCode(exception),
                ValidationErrors = GetValidationErrors(exception),
                Message = GetStatusMessage(exception),
                Status = false,
                Errors = GetErrors(exception)
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = response.HttpCode;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

#pragma warning disable CS8603 // Possible null reference return.
        private static string[] GetErrors(Exception exception) =>
            exception switch
            {
                MyHttpException => new string[] { exception.Message },
                ValidationException => null,
                _ => new string[] { "Something Went Wrong." },
            };
#pragma warning restore CS8603 // Possible null reference return.

        private static string GetStatusMessage(Exception exception) =>
            exception switch
            {
                MyHttpException => exception.Message,
                ValidationException => "",
                _ => "Error"
            };

        private static int GetStatusCode(Exception exception) =>
            exception switch
            {
                ValidationException => StatusCodes.Status422UnprocessableEntity,
                MyHttpException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };

        private static IReadOnlyDictionary<string, string[]> GetValidationErrors(Exception exception)
        {
            IReadOnlyDictionary<string, string[]> errors = null;
            if (exception is ValidationException validationException)
            {
                errors = validationException.ErrorsDictionary;
            }
            return errors;
        }
    }
}
