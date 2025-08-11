using AlignTech.WebAPI.DataFirst.Interfaces;
using AlignTech.WebAPI.DataFirst.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AlignTech.WebAPI.DataFirst.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["AppSettings:token"]!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new List<Claim> {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role)
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                Issuer = _configuration["AppSettings:issuer"],
                Audience = _configuration["AppSettings:audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
