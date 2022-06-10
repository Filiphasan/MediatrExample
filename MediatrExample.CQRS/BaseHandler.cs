using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MediatrExample.CQRS
{
    public class BaseHandler<TRequest, TResponse, THandler>
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        protected readonly ILogger<THandler> _logger;

        public BaseHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<IValidator<TRequest>> validators, ILogger<THandler> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _validators = validators;
            _logger = logger;
        }

        public async Task<Dictionary<string, string[]>> CheckValidate(TRequest request)
        {
            var errorsDictionary = new Dictionary<string, string[]>();
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                errorsDictionary = _validators
                    .Select(x => x.Validate(context))
                    .SelectMany(x => x.Errors)
                    .Where(x => x != null)
                    .GroupBy(
                        x => x.PropertyName,
                        x => x.ErrorMessage,
                        (propertyName, errorMessages) => new
                        {
                            Key = propertyName,
                            Values = errorMessages.Distinct().ToArray()
                        })
                    .ToDictionary(x => x.Key, x => x.Values);                
            }
            return errorsDictionary;
        }
    }
}
