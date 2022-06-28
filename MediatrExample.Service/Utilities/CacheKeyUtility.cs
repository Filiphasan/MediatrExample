using MediatrExample.Shared.Constants;
using MediatrExample.Shared.Enums.CacheEnums;

namespace MediatrExample.Service.Utilities
{
    public class CacheKeyUtility
    {
        private readonly string EnvironmentName;

        public CacheKeyUtility(string environmentName)
        {
            EnvironmentName = environmentName ?? throw new ArgumentNullException(nameof(environmentName));
        }

        public string GetUserCacheKey(UserCacheType userCacheType, object? id = null) =>
            userCacheType switch
            {
                UserCacheType.One => GetCacheKey(CacheKeys.GetUserKey, id.ToString()),
                _ => GetCacheKey(CacheKeys.GetUserListKey)
            };
        

        private string GetCacheKey(string keyFormat, params string[]? formatParameters)
        {
            string result = $"{EnvironmentName.ToLower()}__{keyFormat.ToLower()}";
            if (formatParameters != null)
            {
                result = string.Format(result, formatParameters);
            }
            return result;
        }

    }
}
