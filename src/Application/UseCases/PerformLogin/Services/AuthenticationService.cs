using Application.Shared.Configs;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.UseCases.PerformLogin.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JwtConfiguration _configuration;

        public AuthenticationService(IOptions<JwtConfiguration> configuration)
        {
            _configuration = configuration.Value;
        }

        public string CreateToken(int id, string username, string roles)
        {
            var issuer = _configuration.Issuer;
            var audience = _configuration.Audience;
            var key = Encoding.ASCII.GetBytes(_configuration.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMonths(1),
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                    new Claim(ClaimTypes.Name, username),
                }),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var splitedRoles = roles.Split(';').Select(role => new Claim(ClaimTypes.Role, role));

            foreach (var role in splitedRoles)
                tokenDescriptor.Subject.AddClaim(role);

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
