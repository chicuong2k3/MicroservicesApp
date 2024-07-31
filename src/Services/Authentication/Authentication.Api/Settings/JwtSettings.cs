namespace Authentication.Api.Settings
{
    public class JwtSettings
    {
        public const string Section = "JwtSettings";
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? ExpiresInMinutes { get; set; }
    }
}
