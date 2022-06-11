using Microsoft.Extensions.Primitives;

namespace MediatrExample.API.Middleware
{
    public class CorrelationIdMiddleware
    {
        private const string CorrelationIdKey = "X-Correlation-ID";

        private readonly RequestDelegate _requestDelegate;

        public CorrelationIdMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate)); ;
        }


        public Task Invoke(HttpContext context)
        {
            if (context.Request.Headers.TryGetValue(CorrelationIdKey, out StringValues correlationId))
            {
                context.TraceIdentifier = correlationId;
            }
            else
            {
                string newCorrelationId = Guid.NewGuid().ToString();
                context.Request.Headers.Add(CorrelationIdKey, newCorrelationId);
                context.TraceIdentifier = newCorrelationId;
            }

            return _requestDelegate(context);
        }
    }
}
