using MediatrExample.API.Middleware;

namespace MediatrExample.API.CustomExtensions
{
    public static class MyCustomMiddleware
    {
        /// <summary>
        /// CorrelationId Middleware + CustomeExceptionMiddleware + RequestResponseMiddleware
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMyCustomMiddleware(this WebApplication app)
        {
            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseMiddleware<CustomExceptionHandler>();
            app.UseMiddleware<ReqResLoggingMiddleware>();
            return app;
        }
    }
}
