namespace Authentication.Api.Services
{
    public interface ITokenService
    {
        Task<string> GenerateAccessTokenAsync(AppUser user);
    }
}
