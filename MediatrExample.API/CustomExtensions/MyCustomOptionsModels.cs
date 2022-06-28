using MediatrExample.Shared.OptionModel;

namespace MediatrExample.API.CustomExtensions
{
    public static class MyCustomOptionsModels
    {
        /// <summary>
        /// Configure My Options Class for Options Pattern
        /// </summary>
        /// <param name="services">Extension Parameter</param>
        /// <param name="configuration">IConfiguration instance, for get configure value from appsetting</param>
        /// <returns>
        /// <list type="number">
        /// <item>
        ///     MyTokenOptions for JWT Setting
        /// </item>
        /// </list>
        /// </returns>
        public static IServiceCollection ConfigureMyOptionModels(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MyTokenOptions>(configuration.GetSection("JWTTokenOptions"));
            return services;
        }
    }
}
