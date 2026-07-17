using FluentValidation;
using MediatR;
using AppValidationException =
    Ecommerce.Application.Common.Exceptions.ValidationException;

namespace Ecommerce.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var results = await Task.WhenAll(
                    _validators.Select(x => x.ValidateAsync(context, cancellationToken)));

            var failures = results
                .SelectMany(x => x.Errors)
                .Where(x => x is not null)
                .ToList();

            var errors = failures
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray());

            if (failures.Any())
                throw new AppValidationException(errors);

            return await next();
        }
    }
}
