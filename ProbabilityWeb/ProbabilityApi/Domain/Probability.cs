using ProbabilityApi.Models.Enums;
using ProbabilityApi.Models.Requests;

namespace ProbabilityApi.Domain;

public sealed class Probability
{
	public double A { get; private set; }
	public double B { get; private set; }
	public ProbabilityType Type { get; private set; } = ProbabilityType.Unknown;
	public double CalculationResult => CalculateProbability();
	
	// In this particular case we want to create a Probability only from the GetProbabilityHandler
	// So a static constructor taking that DTO explicitly makes more sense.
	public static Probability FromCalculateProbability(CalculateProbability p)
		=> new Probability(p.A, p.B, p.Type);

	public override string ToString()
		=> $"[{Type}] - [A: {A}, B: {B}] - [Result: {CalculateProbability()}]";

	private Probability(double A, double B, ProbabilityType type)
	{
		this.A = A;
		this.B = B;
		this.Type = type;
	}

	// Decided to round to two decimal digits for clarity
	private double And() => Math.Round(A * B, 2);
	private double Or() => Math.Round(A + B - A * B, 2);

	private double CalculateProbability() => Type switch
	{
		ProbabilityType.And => And(),
		ProbabilityType.Or  => Or(),
		ProbabilityType.Unknown => 0.0,
		_ => throw new ArgumentOutOfRangeException(nameof(Type), $"Unexpected probability type value: {Type}"),
	};
}
