using FluentValidation;
using ProbabilityApi.Models.Requests;

namespace ProbabilityApi.Validators;

public class CalculateProbabilityValidator : AbstractValidator<CalculateProbability>
{
	public CalculateProbabilityValidator()
	{
		RuleLevelCascadeMode = CascadeMode.Stop;

		RuleFor(x => x.A)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(1);	

		RuleFor(x => x.B)
			.NotNull()
			.GreaterThanOrEqualTo(0)
			.LessThanOrEqualTo(1);	

		RuleFor(x => x.Type)
			.NotNull()
			.NotEmpty();

		// TODO: We probably want to use .WithMessage() or .WithErrorCode() in production level code
	}
}
