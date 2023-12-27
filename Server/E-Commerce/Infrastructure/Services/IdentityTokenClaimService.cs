using Application.Common;
using Application.Interfaces;
using Domain.Entities.Security;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TokenClaimService: ITokenClaimService
    {
        private readonly AppConfig _config;

        public TokenClaimService(IOptions<AppConfig> config)
        {
            _config = config.Value;
        }

        public async Task<string> GetTokenAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.ApiKey);

            //var roles = user.UserRole; // Replace this with your way of getting roles

            //var claims = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim("UserId", user.Id.ToString()),
            //};


            var claims = new List<Claim> { new Claim("Name", user.UserName) };
            claims.Add(new Claim("UserId", user.Id.ToString()));
           
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _config.Issuer,
                Audience = _config.Audience,
                Subject = new ClaimsIdentity(claims.ToArray()),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
