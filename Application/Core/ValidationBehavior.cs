
using FluentValidation;
using MediatR;

namespace Application.Core;

public class ValidationBehaviour<TRequest, TResponse>(IValidator<TRequest>? validator = null)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (validator == null) return await next();

        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        return await next();
    }
}
// This is a validation middleware (or more accurately, a pipeline behavior) for MediatR using FluentValidation

// 1. The ValidationBehavior Class
// This implements IPipelineBehavior<TRequest, TResponse>, which means it intercepts MediatR requests before they reach their respective handlers.
// The constructor takes an optional IValidator<TRequest> (which FluentValidation uses to validate the request object).
// Inside Handle:
// If no validator exists, it simply calls the next delegate (next()) to proceed to the handler.
// If a validator does exist, it validates the request asynchronously.
// If validation fails, it throws a ValidationException with the errors.
// If validation succeeds, it continues to the next step in the pipeline.
// 2. Registration in Program.cs