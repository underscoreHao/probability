using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ProbabilityApi.Handlers.Requests;
using ProbabilityApi.Models.Requests;
using ProbabilityApi.Models.Responses;
using Xunit;

namespace ProbabilityApi.Tests;

public class CalculateProbabilityHandlerTests
{
	private readonly CalculateProbabilityHandler _sut;
	private readonly ILogger<CalculateProbabilityHandler> _logger = Substitute.For<ILogger<CalculateProbabilityHandler>>();

	public CalculateProbabilityHandlerTests()
		=> _sut = new CalculateProbabilityHandler(_logger); 

    [Fact]
    public async Task Handler_ShouldReturn_CalculateProbabilityResult()
    {
		// Arrange
		var fixture = new Fixture();
		var request = fixture.Create<CalculateProbability>();
		var response = fixture.Create<CalculateProbabilityResponse>();
		
		// Act
		var result = await _sut.Handle(request, default);

		// Assert
		result.Should().NotBeNull();
		result.Should().BeOfType<CalculateProbabilityResponse>();
    }
}
