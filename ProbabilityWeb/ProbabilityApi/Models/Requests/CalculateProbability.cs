using MediatR;
using ProbabilityApi.Models.Enums;
using ProbabilityApi.Models.Responses;

namespace ProbabilityApi.Models.Requests;

public class CalculateProbability : IRequest<CalculateProbabilityResponse>
{
	// I'm using FluentValidation which will run when the MediatR request handler is invoked (via a behavior pipeline)
	// With that said, I believe that in some cases we can (and should) fail earlier. In those cases I'd use data anotations on the DTO itself
	// Attributes like [Required], [Range] etc.
	public double A { get; set; }
	public double B { get; set; }
	public ProbabilityType Type { get; set; } = ProbabilityType.Unknown;
}
