using MediatrExample.Shared.OptionModel;

namespace MediatrExample.API.CustomExtensions
{
    public static class MyCustomOptionsModels
    {
        public static IServiceCollection ConfigureMyOptionModels(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MyTokenOptions>(configuration.GetSection("JWTTokenOptions"));
            return services;
        }
    }
}
