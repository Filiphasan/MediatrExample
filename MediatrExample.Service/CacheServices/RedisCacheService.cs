using MediatrExample.Core.Interfaces.Service.CacheService;
using StackExchange.Redis;
using System.Text.Json;

namespace MediatrExample.Service.CacheServices
{
    public class RedisCacheService : IRedisCacheService
    {
        private IDatabase _database;
        private static IConnectionMultiplexer _connectionMultiplexer;
        public RedisCacheService(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer ?? throw new ArgumentNullException(nameof(connectionMultiplexer));
            _database = _connectionMultiplexer.GetDatabase(0);
        }

        public async Task<T> GetAsync<T>(string key)
        {
            return default;
        }

        public async Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public async Task SetAsync<T>(string key, T value, int expiredInMinute = 120)
        {
            RedisKey redisKey = new RedisKey(key);
            var valueStr = JsonSerializer.Serialize(value);
            RedisValue redisValue = new RedisValue(valueStr);
            await _database.StringSetAsync(redisKey, redisValue, TimeSpan.FromMinutes(expiredInMinute));
        }

        public async Task<TimeSpan> PingAsync()
        {
            return await _database.PingAsync();
        }
    }
}
