using Carter;
using Common.Exceptions.Handlers;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Extensions;

namespace Ordering.Api;

public static class DIExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddCarter();

        services.AddExceptionHandler<CustomExceptionHandler>();

        services.AddHealthChecks().AddSqlServer(configuration.GetConnectionString("SqlServer")!);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseHttpsRedirection();


        if (app.Environment.IsDevelopment())
        {
            app.InitializeDatabase();
        }

        app.MapCarter();

        app.UseExceptionHandler(options =>
        {

        });

        app.UseHealthChecks("/api/health", new HealthCheckOptions()
        {
            ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });

        return app;
    }
}
