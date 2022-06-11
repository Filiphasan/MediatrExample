using MediatrExample.Core.Interfaces.Service;
using Microsoft.Extensions.Logging;

namespace MediatrExample.Service
{
    public class BaseService<TService> where TService : class, IService, new()
    {
        protected readonly ILogger<TService> _logger;

        public BaseService(ILogger<TService> logger)
        {
            _logger = logger;
        }
    }
}
