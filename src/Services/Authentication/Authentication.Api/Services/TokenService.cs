using Authentication.Api.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Api.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<AppUser> userManager;
        private readonly JwtSettings jwtSettings;
        public TokenService(
            UserManager<AppUser> userManager,
            IOptionsMonitor<JwtSettings> jwtSettingsOptions)
        {
            this.userManager = userManager;
            this.jwtSettings = jwtSettingsOptions.CurrentValue;
        }
        public async Task<string> GenerateAccessTokenAsync(AppUser user)
        {
            var secretKey = Environment.GetEnvironmentVariable("JWT-SECRET");

            if (string.IsNullOrEmpty(secretKey))
            {
                throw new ArgumentNullException("Secret Key does not exist.");
            }

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };

            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenOptions = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.ExpiresInMinutes)),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        }
    }
}
