using Authentication.Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Auth;

public record LoginResponse(
    string Id,
    string? Email,
    string FirstName,
    string LastName,
    string Token);
public record LoginRequest(string UserName, string Password);
public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty();
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
public class LoginEndpoint(
    UserManager<AppUser> userManager,
    ITokenService tokenService) 
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/login", async ([FromBody] LoginRequest request) =>
        {
            var user = await userManager.FindByNameAsync(request.UserName);

            if (user == null)
            {
                return Results.BadRequest();
            }

            var isValid = await userManager.CheckPasswordAsync(user, request.Password);

            if (!isValid)
            {
                return Results.BadRequest();
            }

            var token = await tokenService.GenerateAccessTokenAsync(user);

            var response = new LoginResponse(user.Id, user.Email, user.FirstName, user.LastName, token);
            return Results.Ok(response);
        })
        .WithName("Login")
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Login");
    }
}
