using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace MediatrExample.Service.Utilities
{
    public static class RedisUtility
    {
        public static ConnectionMultiplexer ConnectRedis(IConfiguration Configuration)
        {
            var redisOptions = ConfigurationOptions.Parse($"{Configuration["Redis:Host"]}:{Configuration["Redis:Port"]}");
            redisOptions.Password = Configuration["Redis:Password"];
            redisOptions.AbortOnConnectFail = false;
            var multiplexer = ConnectionMultiplexer.Connect(redisOptions);
            return multiplexer;
        }
    }
}
