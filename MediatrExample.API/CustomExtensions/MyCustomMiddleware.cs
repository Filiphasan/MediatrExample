using MediatrExample.API.Middleware;

namespace MediatrExample.API.CustomExtensions
{
    public static class MyCustomMiddleware
    {
        /// <summary>
        /// CorrelationId Middleware + CustomeExceptionMiddleware + RequestResponseMiddleware
        /// </summary>
        /// <param name="app"></param>
        /// <returns>
        /// <list type="number">
        /// <item>
        ///     CorrelationId Middleware for Add Request&Response Track Identifier
        /// </item>
        /// <item>
        ///     CustomExceptionHandler Middleware for Prepare Exception Response
        /// </item>
        /// <item>
        ///     UnAuthorizedResponseMiddleware Middleware for Add Prepare UnAuthorize Response
        /// </item>
        /// </list>
        /// </returns>
        public static IApplicationBuilder UseMyCustomMiddleware(this WebApplication app)
        {
            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseMiddleware<CustomExceptionHandler>();
            app.UseMiddleware<UnAuthorizedResponseMiddleware>();
            //app.UseMiddleware<ReqResLoggingMiddleware>();
            return app;
        }
    }
}
