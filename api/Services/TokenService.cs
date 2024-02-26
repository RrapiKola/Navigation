using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using Microsoft.IdentityModel.Tokens;

namespace api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey symmetricSecurityKey;
        
        public TokenService(IConfiguration configuration)
        {
            symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigninKey"]));
            
            this.configuration = configuration;
            
        }



        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Email,appUser.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,appUser.UserName)
            };

            var credetials = new SigningCredentials(symmetricSecurityKey,SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires= DateTime.Now.AddDays(7),
                SigningCredentials = credetials,
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}