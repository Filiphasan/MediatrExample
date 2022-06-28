﻿using MediatrExample.API.Middleware;
using MediatrExample.Core.Interfaces.Data;
using MediatrExample.Core.Interfaces.Service;
using MediatrExample.Core.Interfaces.Service.CacheService;
using MediatrExample.Data.Repositories;
using MediatrExample.Service.CacheServices;
using MediatrExample.Service.HelpServices;

namespace MediatrExample.API.CustomExtensions
{
    public static class MyCustomerServiceLife
    {
        public static IServiceCollection AddMyServiceLifeCycles(this IServiceCollection services)
        {
            services.AddScoped<IRedisCacheService, RedisCacheService>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<CustomExceptionHandler>();
            services.AddTransient<IHashService, HashService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient(typeof(ILogHelper<>), typeof(LogHelper<>));
            return services;
        }
    }
}
