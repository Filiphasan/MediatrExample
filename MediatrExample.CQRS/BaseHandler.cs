using FluentValidation;
using MediatrExample.Core.Interfaces.Service;
using Microsoft.AspNetCore.Http;
using ValidationException = MediatrExample.Shared.CustomExceptions.ValidationException;

namespace MediatrExample.CQRS
{
    public class BaseHandler<TRequest, TResponse, THandler>
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        protected readonly ILogHelper<THandler> _logHelper;

        public BaseHandler(IHttpContextAccessor httpContextAccessor, IEnumerable<IValidator<TRequest>> validators, ILogHelper<THandler> logHelper)
        {
            _httpContextAccessor = httpContextAccessor;
            _validators = validators;
            _logHelper = logHelper;
        }

        public Task CheckValidate(TRequest request)
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
            if (errorsDictionary.Any())
            {
                throw new ValidationException(errorsDictionary);
            }

            return Task.CompletedTask;
        }
    }
}
