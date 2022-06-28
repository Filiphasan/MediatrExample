using MediatrExample.Shared.DataModels;
using System.Net;
using System.Text.Json;

namespace MediatrExample.API.Middleware
{
    public class UnAuthorizedResponseMiddleware
    {

        private readonly RequestDelegate _next;

        public UnAuthorizedResponseMiddleware(RequestDelegate requestDelegate)
        {
            _next = requestDelegate ?? throw new ArgumentNullException(nameof(requestDelegate)); ;
        }
        

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(GenericResponse<NoData>.Error(HttpStatusCode.Unauthorized.GetHashCode(), "UnAuthorized")));
            }
        }
    }
}
