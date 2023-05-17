using ProbabilityApi.Models.Enums;

namespace ProbabilityApi.Models.Responses;

public class CalculateProbabilityResponse
{
	public double A { get; set; }
	public double B { get; set; }
	public ProbabilityType Type { get; set; } = ProbabilityType.Unknown;
	public double Result { get; set; }
	public string Log { get; set; } = string.Empty;
}
