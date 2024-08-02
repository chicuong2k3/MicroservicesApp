using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("customPolicy", opts =>
    {
        opts.PermitLimit = 5;
        opts.QueueLimit = 5;
        opts.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        opts.Window = TimeSpan.FromSeconds(5);

    });
});


builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();


app.UseRateLimiter();
app.MapReverseProxy();


app.Run();
