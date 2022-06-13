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

                //await LogRequest(context, correlationId);

                await _next(context);

                //await LogResponse(context, correlationId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Request Response Logging Middleware");
            }
        }

        private async Task LogRequest(HttpContext context, string correlationId)
        {
            var request = context.Request;

            var requestBody = await ReadStreamInChunks(request.Body);

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"CorrelationId: {correlationId}");
            stringBuilder.AppendLine($"Method: {request.Method}");
            stringBuilder.AppendLine($"Path: {request.Path}");
            stringBuilder.AppendLine($"QueryString: { request.QueryString }");
            stringBuilder.AppendLine($"RequestBody: { requestBody }");

            _logger.LogInformation(stringBuilder.ToString());
        }

        private async Task LogResponse(HttpContext context, string correlationId)
        {
            var response = context.Response;
            

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"CorrelationId: {correlationId}");

            _logger.LogInformation(stringBuilder.ToString());
        }

        private async Task<string> ReadStreamInChunks(Stream stream)
        {
            var newStream = new MemoryStream();
            await stream.CopyToAsync(newStream);
            newStream.Seek(0, SeekOrigin.Begin);
            var reader = new StreamReader(newStream, Encoding.UTF8);
            var body = reader.ReadToEnd();
            return body ?? string.Empty;
        }
    }
}
