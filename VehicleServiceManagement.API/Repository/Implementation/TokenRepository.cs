using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VehicleServiceManagement.API.Repository.Interface;

namespace VehicleServiceManagement.API.Repository.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public string CreateJwtToken(IdentityUser user, string role)
        {

            // create claims

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,role)
            };


            // JWT security Token Parameter

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires:DateTime.Now.AddMinutes(15),
                    signingCredentials:credentials
                );

            // Return Token


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
