using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace MediatrExample.CQRS
{
    public class BaseHandler<TRequest, TResponse>
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public BaseHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<IValidator<TRequest>> validators)
        {
            _httpContextAccessor = httpContextAccessor;
            _validators = validators;
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
