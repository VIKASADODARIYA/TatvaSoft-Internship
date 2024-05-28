using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.JwtService
{
    public class JWTService
    {
        private readonly string _secretKey;
        private readonly int _tokenDuration;

        public JWTService(IConfiguration configuration)
        {
            _secretKey = configuration.GetValue<string>("JwtConfig:Key");
            _tokenDuration = configuration.GetValue<int>("JwtConfig:Duration"); 
        }

        public string GenerateToken(string name,string email, bool isAdmin)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var role = "";
            if(isAdmin)
            {
                role = "Admin";
            }
            else
            {
                role = "User";
            }
            var claims = new[]
            {
                new Claim("name",name),
                new Claim("email", email),
                new Claim(ClaimTypes.Role,role)
            };

            var token = new JwtSecurityToken(
                  issuer: "localhost",
                  audience: "localhost",
                  claims: claims,
                  expires: DateTime.Now.AddHours(_tokenDuration),
                  signingCredentials:credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
