using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Services.Implementation
{
    public class JWTService
    {
        private static IConfigurationRoot _config;
        static JWTService()
        {
            InitializeObjects();
        }
        public static string GenerateToken(ClaimsParameters claimsParameters)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = GetClaims(claimsParameters);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        private static Claim[] GetClaims(ClaimsParameters claimsParameters )
        {
            return new Claim[]{
                new Claim(ClaimTypes.NameIdentifier, claimsParameters.Id),                
                new Claim(ClaimTypes.Role, claimsParameters.UserRole),
            };
        }

        private static void InitializeObjects()
        {
            _config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", true, true)
                       .Build();
        }
    }
    public class ClaimsParameters
    {
        public string Id { get; set; }        
        public string UserRole { get; set; }
    }
}
