
using Common.Behaviours;
using Common.Exceptions.Handlers;
using Discount.gRPC.Protos;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Reflection;
using Weasel.Core;


var builder = WebApplication.CreateBuilder(args);

// Application Services
var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddCarter();

// Data Services
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


// Grpc Services

builder.Services.AddGrpcClient<DiscountProto.DiscountProtoClient>(options =>
{
    var url = builder.Configuration["GrpcSettings:DiscountUrl"]!;
    options.Address = new Uri(url);
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    var handler = new HttpClientHandler()
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };

    return handler;
});


// Cross-cutting Services

if (builder.Environment.IsDevelopment())
{
}

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
