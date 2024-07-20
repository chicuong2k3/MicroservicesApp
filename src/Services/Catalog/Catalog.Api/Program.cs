
using Catalog.Api.Data;
using Common.Behaviours;
using Common.Exceptions.Handlers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

var martenConnectionStr = builder.Configuration.GetConnectionString("Marten")!;

builder.Services.AddMarten(options =>
{
    options.Connection(martenConnectionStr);
    options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
    options.UseSystemTextJsonForSerialization();
    

}).UseLightweightSessions();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<DataInitial>();
}

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
    .AddNpgSql(martenConnectionStr);

var app = builder.Build();

app.UseExceptionHandler(options =>
{

});

app.UseHealthChecks("/api/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

//app.UseExceptionHandler(exceptionHandler =>
//{
//    exceptionHandler.Run(async context =>
//    {
//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

//        if (exception == null) return;

//        var problemDetails = new ProblemDetails()
//        {
//            Title = exception.Message,
//            Status = StatusCodes.Status500InternalServerError,
//            Detail = exception.StackTrace
//        };

//        //var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        //logger.LogError(exception, exception.Message);

//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/problem+json";

//        await context.Response.WriteAsJsonAsync(problemDetails);
//    });
//});

app.MapCarter();

app.Run();
