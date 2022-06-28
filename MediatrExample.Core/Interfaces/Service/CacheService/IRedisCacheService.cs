namespace MediatrExample.Core.Interfaces.Service.CacheService
{
    public interface IRedisCacheService : ICacheService
    {
        Task<TimeSpan> PingAsync();
    }
}
