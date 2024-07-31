using Authentication.Api.Data;
using Authentication.Api.Services;
using Authentication.Api.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("ApiSettings:JwtSettings"));

builder.Services.AddTransient<ITokenService, TokenService>();

var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();


app.Run();
