
using Marten.Linq.CreatedAt;
using Microsoft.AspNetCore.Http.HttpResults;
using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter();

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
});

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Marten")!);
    options.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
    options.UseSystemTextJsonForSerialization();
    

}).UseLightweightSessions();

var app = builder.Build();

app.MapCarter();

app.Run();
