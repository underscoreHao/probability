using System.Threading.Tasks;
using FluentAssertions;
using ProbabilityApi.Models.Enums;
using ProbabilityApi.Models.Requests;
using ProbabilityApi.Validators;
using Xunit;

namespace ProbabilityApi.Tests;

public class CalculateProbabilityValidatorTests
{
    private readonly CalculateProbabilityValidator _sut;

    public CalculateProbabilityValidatorTests()
        => _sut = new CalculateProbabilityValidator();

    [Theory]
    [MemberData(nameof(ValidData))]
    public async Task CalculateProbabilityValidator_ShouldReturn_IsValid(ProbabilityType type, double a, double b)
    {
        // Arrange
        CalculateProbability probability = new()
        {
            A = a,
            B = b,
            Type = type,
        };

        // Act
        var result = await _sut.ValidateAsync(probability);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [MemberData(nameof(InvalidData))]
    public async Task CalculateProbabilityValidator_ShouldReturn_IsNotValid(ProbabilityType type, double a, double b)
    {
        // Arrange
        CalculateProbability probability = new()
        {
            A = a,
            B = b,
            Type = type,
        };

        // Act
        var result = await _sut.ValidateAsync(probability);

        // Assert
        result.Should().NotBeNull();
        result.IsValid.Should().BeFalse();
    }

    public static TheoryData<ProbabilityType, double, double> ValidData => new()
	{
		{ ProbabilityType.And, 0.0, 0.0 },
		{ ProbabilityType.And, 0.5, 0.5 },
		{ ProbabilityType.And, 0.8, 0.92 },
		{ ProbabilityType.Or, 0.0, 0.0 },
		{ ProbabilityType.Or, 0.5, 0.5 },
		{ ProbabilityType.Or, 0.9, 0.9 },
	};

    public static TheoryData<ProbabilityType, double, double> InvalidData => new()
	{
		{ ProbabilityType.Unknown, 0.0, 0.0 },
		{ ProbabilityType.And, -0.5, 0.5 },
		{ ProbabilityType.And, 0.8, 2.92 },
		{ ProbabilityType.Unknown, -2.0, 0.0 },
		{ ProbabilityType.Unknown, 0.0, 30.0 },
		{ ProbabilityType.Or, double.MaxValue, 30.0 },
		{ ProbabilityType.And, 0, double.MinValue },
	};
}
