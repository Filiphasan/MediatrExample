using MediatrExample.API.Middleware;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Data.Repositories;

namespace MediatrExample.API.CustomExtensions
{
    public static class MyCustomerServiceLife
    {
        public static IServiceCollection AddMyServiceLifeCycles(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<CustomExceptionHandler>();
            return services;
        }
    }
}
