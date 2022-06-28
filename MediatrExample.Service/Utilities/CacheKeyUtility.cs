namespace MediatrExample.Service.Utilities
{
    public class CacheKeyUtility
    {
        private readonly string EnvironmentName;

        public CacheKeyUtility(string environmentName)
        {
            EnvironmentName = environmentName ?? throw new ArgumentNullException(nameof(environmentName));
        }

        public string GetCacheKey(string key)
        {
            string result = $"{EnvironmentName.ToLower()}__{key.ToLower()}";
            return result;
        }

        public string GetCacheKey(string keyFormat, object? id)
        {
            string result = $"{EnvironmentName.ToLower()}__{keyFormat.ToLower()}";
            if (id != null)
            {
                result = string.Format(result, id);
            }
            return result;
        }

        public string GetCacheKey(string keyFormat, params string[]? formatParameters)
        {
            string result = $"{EnvironmentName.ToLower()}__{keyFormat.ToLower()}";
            if (formatParameters != null && formatParameters.Any())
            {
                result = string.Format(result, formatParameters);
            }
            return result;
        }

    }
}
