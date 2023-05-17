using System.Text.Json.Serialization;

namespace ProbabilityApi.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ProbabilityType
{
	Unknown, // I always set a default first value for my enums, I think it's cleaner than having a nullable property
	And,
	Or
}
