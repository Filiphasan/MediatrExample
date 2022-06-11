using MediatrExample.Core.Interfaces.Service;
using Microsoft.Extensions.Logging;

namespace MediatrExample.Service
{
    public class BaseService<TService>
    {
        protected readonly ILogger<TService> _logger;

        public BaseService(ILogger<TService> logger)
        {
            _logger = logger;
        }
    }
}
