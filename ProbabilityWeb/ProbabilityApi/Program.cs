using System.Reflection;
using FluentValidation;
using MediatR;
using ProbabilityApi.Behaviors;
using ProbabilityApi.Models.Requests;
using ProbabilityApi.Models.Responses;

const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to container.

// MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// FluentValidation
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Enable CORS
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
		builder => {
			builder.WithOrigins("*");
		});
});

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);

app.MapGet("/", () => "Hello Probability!");

// I've decided to go with minimal API since the task doesn't really warrant a full blown controller
app.MapPost("/calculateProbability", async (CalculateProbability probability, IMediator mediator) => {
    return await mediator.Send(probability) is CalculateProbabilityResponse result
		? Results.Ok(result)
		: Results.BadRequest();
});

app.Run();
