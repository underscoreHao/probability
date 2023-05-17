using FluentValidation;
using MediatR;

namespace ProbabilityApi.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	where TRequest : IRequest<TResponse>
{
	private readonly IEnumerable<IValidator<TRequest>> _validators;
	
	public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
		=> _validators = validators;

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
	{
		if (_validators.Any())
		{
			var context = new ValidationContext<TRequest>(request);
			var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, ct)));

			var failures = validationResults
				.SelectMany(r => r.Errors)
				.Where(f => f is not null)
				.ToList();

			// TODO: Usually these exceptions will be wrapped by a middleware mapping any issues to a ProblemDetails object
			// OR our results object will contain errors collection in the model etc.
			if (failures.Any())
				throw new ValidationException(failures);
		}

		return await next();
	}
}
