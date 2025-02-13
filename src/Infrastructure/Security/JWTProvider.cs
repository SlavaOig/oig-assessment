using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using oig_assessment.Common;
using oig_assessment.Domain.Entities;

namespace oig_assessment.Infrastructure.Security
{
    public class JwtProvider
    {
        private readonly LocalLoginSettings _loginSettings;

        public JwtProvider(IOptions<LocalLoginSettings> loginSettings)
        {
            _loginSettings = loginSettings.Value;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_loginSettings.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.LoginName),
                new Claim("role", user.Role.Name),
                new Claim("userId", user.Id.Value.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _loginSettings.Issuer,
                audience: _loginSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
