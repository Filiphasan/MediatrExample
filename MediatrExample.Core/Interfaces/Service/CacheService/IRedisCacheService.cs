namespace MediatrExample.Core.Interfaces.Service.CacheService
{
    public interface IRedisCacheService : ICacheService
    {
        /// <summary>
        /// Ping Redis Server
        /// </summary>
        /// <returns>TimeSpan latency</returns>
        Task<TimeSpan> PingAsync();
    }
}
