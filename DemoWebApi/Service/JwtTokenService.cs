using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Service
{
    public class JwtTokenService
    {
        private const string SecurityAlgorithm = SecurityAlgorithms.HmacSha256;

        private readonly IOptions<JwtConfig> _config;

        public JwtTokenService(IOptions<JwtConfig> jwtConfig)
        {
            _config = jwtConfig;
        }

        public string CreateAccessToken(string id)
        {
            var claims = new List<Claim>
            {
                new Claim("id", id),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.Value.secret));
            var creds = new SigningCredentials(key, SecurityAlgorithm);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_config.Value.AccessTokenLifetime),
                SigningCredentials = creds,
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var randomGenerator = RandomNumberGenerator.Create())
            {
                randomGenerator.GetBytes(randomNumber);
                return new RefreshToken()
                {
                    Token = Convert.ToBase64String(randomNumber),
                    ExpiresAt = DateTime.UtcNow.Add(_config.Value.RefreshTokenLifetime)
                };
            }
        }

        public struct RefreshToken
        {
            public string Token { get; set;}
            public DateTime ExpiresAt { get; set;}
        }
    }
}