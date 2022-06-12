using System.Text;

namespace MediatrExample.API.Middleware
{
    public class ReqResLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ReqResLoggingMiddleware> _logger;

        public ReqResLoggingMiddleware(RequestDelegate next, ILogger<ReqResLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var correlationId = context.Request.Headers["X-Correlation-ID"];

                await LogRequest(context, correlationId);

                await _next.Invoke(context);

                await LogResponse(context, correlationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Request Response Logging Middleware");
            }
        }

        private async Task LogRequest(HttpContext context, string correlationId)
        {
            var request = context.Request;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"CorrelationId: {correlationId}");
            stringBuilder.AppendLine($"Method: {request.Method}");
        }

        private async Task LogResponse(HttpContext context, string correlationId)
        {
            var response = context.Response;

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"CorrelationId: {correlationId}");
            stringBuilder.AppendLine($"Method: {response.}");
        }
    }
}
