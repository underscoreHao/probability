using MediatR;
using ProbabilityApi.Domain;
using ProbabilityApi.Utils;
using ProbabilityApi.Models.Requests;
using ProbabilityApi.Models.Responses;

namespace ProbabilityApi.Handlers.Requests;

public class CalculateProbabilityHandler : IRequestHandler<CalculateProbability, CalculateProbabilityResponse>
{
	private readonly ILogger<CalculateProbabilityHandler> _logger;

	public CalculateProbabilityHandler(ILogger<CalculateProbabilityHandler> logger)
		=> _logger = logger;

	public Task<CalculateProbabilityResponse> Handle(CalculateProbability request, CancellationToken cancellationToken)
	{
		var probability = Probability.FromCalculateProbability(request);
		var logMsg = probability.ToString();

		FileLogger.Log(logMsg);

		_logger.LogInformation($"Calculating probability: {logMsg}");

		// Depending on the use case it is OK to expose a read-only version of the domain object back to the controller.
		// In this case I just return a simple response DTO	
		CalculateProbabilityResponse response = new()
		{
			A = probability.A,
			B = probability.B,
			Type = probability.Type,
			Result = probability.CalculationResult,
			Log = $"{DateTime.UtcNow} - {logMsg}"
		};

		return Task.FromResult(response);
	}
}
