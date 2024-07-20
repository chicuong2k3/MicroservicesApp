
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

    options.Schema.For<Cart>().Identity(x => x.UserName);


}).UseLightweightSessions();



builder.Services.AddScoped<ICartRepository, CartRepository>();

var redisConnectionStr = builder.Configuration.GetConnectionString("Redis")!;
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnectionStr;
});

builder.Services.Decorate<ICartRepository, CachedCartRepository>();



if (builder.Environment.IsDevelopment())
{
}

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();



builder.Services.AddHealthChecks()
    .AddNpgSql(martenConnectionStr)
    .AddRedis(redisConnectionStr);

var app = builder.Build();

app.UseExceptionHandler(options =>
{

});

app.UseHealthChecks("/api/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});


app.MapCarter();

app.Run();
