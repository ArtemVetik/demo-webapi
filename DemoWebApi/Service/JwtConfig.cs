namespace Service
{
    public class JwtConfig
    {
        public string secret { get; set; }
        public string accessTokenExpirationMinutes { get; set; }
        public string refreshTokenExpirationDays { get; set; }

        public TimeSpan AccessTokenLifetime => TimeSpan.FromMinutes(int.Parse(accessTokenExpirationMinutes));
        public TimeSpan RefreshTokenLifetime => TimeSpan.FromDays(int.Parse(refreshTokenExpirationDays));
    }
}