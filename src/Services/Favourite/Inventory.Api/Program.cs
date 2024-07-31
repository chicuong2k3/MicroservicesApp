

using Common.Behaviours;
using Common.Exceptions.Handlers;
using Favourite.Api.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var assembly = Assembly.GetExecutingAssembly();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);


builder.Services.AddTransient<IFavouriteItemRepository, FavouriteItemRepository>();


builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

//builder.Services.AddHealthChecks()
//    .AddNpgSql(martenConnectionStr);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseExceptionHandler(options =>
{

});


//app.UseHealthChecks("/api/health", new HealthCheckOptions()
//{
//    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
//});

app.MapCarter();

app.Run();

