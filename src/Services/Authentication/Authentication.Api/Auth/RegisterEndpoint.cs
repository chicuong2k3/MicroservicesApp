using Authentication.Api.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Auth;

public record RegisterResponse(
    string Id,
    string Email,
    string FirstName,
    string LastName);
public record RegisterRequest(
    string UserName, 
    string Password,
    string Email,
    string FirstName,
    string LastName);
public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty();
        RuleFor(x => x.Password)
            .NotEmpty();
    }
}
public class RegisterEndpoint(UserManager<AppUser> userManager) 
    : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/auth/register", async ([FromBody] RegisterRequest request) =>
        {
            var user = new AppUser
            {
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName
            };

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                return Results.BadRequest(result.Errors);
            }

            var response = new RegisterResponse(
                user.Id, 
                user.Email, 
                user.FirstName, 
                user.LastName);

            return Results.Ok(response);
        })
        .WithName("Register")
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Register");
    }
}
