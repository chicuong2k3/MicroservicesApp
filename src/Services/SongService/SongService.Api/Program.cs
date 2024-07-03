
using Common.Behaviours;
using Common.Exceptions.Handlers;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Marten")!);
    options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
    options.UseSystemTextJsonForSerialization();
    

}).UseLightweightSessions();


builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler(options =>
{

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
