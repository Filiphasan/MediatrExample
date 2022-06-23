using MediatrExample.Core.Interfaces.Service.CacheService;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System.Text;
using System.Text.Json;

namespace MediatrExample.Service.CacheServices
{
    public class RedisCacheService : IRedisCacheService
    {
        private IDatabase _database;
        private readonly RedisCacheOptions _redisCacheOptions;
        private static ConnectionMultiplexer _connectionMultiplexer;
        private readonly IConfiguration _configuration;
        public RedisCacheService(IConfiguration configuration)
        {
            _configuration = configuration;
            _redisCacheOptions = new RedisCacheOptions()
            {
                ConfigurationOptions = new ConfigurationOptions
                {
                    EndPoints = { $"{_configuration["Redis:Host"]}:{_configuration["Redis:Port"]}"},
                    Password = _configuration["Redis:Password"],
                    //Ssl = true,
                    //AbortOnConnectFail = false
                }
            };
            _connectionMultiplexer = ConnectionMultiplexer.Connect(_redisCacheOptions.ConfigurationOptions);
            _database = _connectionMultiplexer.GetDatabase();
            
        }

        public async Task<T> GetAsync<T>(string key)
        {
            using var redisCache = new RedisCache(_redisCacheOptions);
            var valueyteArr = await redisCache.GetAsync(key);
            var valueStr = Encoding.UTF8.GetString(valueyteArr);
            if (!string.IsNullOrWhiteSpace(valueStr))
            {
                using var stream = new MemoryStream(valueyteArr);
                return await JsonSerializer.DeserializeAsync<T>(stream); 
            }
            return default(T);
        }

        public async Task RemoveAsync(string key)
        {
            using var redisCache = new RedisCache(_redisCacheOptions);
            await redisCache.RemoveAsync(key);
        }

        public async Task SetAsync<T>(string key, T value, int expiredInMinute = 120)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(expiredInMinute)
            };
            using var redisCache = new RedisCache(_redisCacheOptions);
            var valueByteArr = JsonSerializer.SerializeToUtf8Bytes(value);
            await redisCache.SetAsync(key, valueByteArr, cacheOptions);
        }
    }
}
