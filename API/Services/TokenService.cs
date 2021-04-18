using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey  _key;
        public TokenService(IConfiguration Config)
        {
            _key= new  SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["TokenKey"]));
        }

        public string CreateToken(AppUser appUser)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.NameId,appUser.UserName),
            };

            var creds = new SigningCredentials(_key,SecurityAlgorithms.HmacSha512Signature);

            var TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(4),
                SigningCredentials=creds
            };

            var TokenHandler = new JwtSecurityTokenHandler(); 

            var Token = TokenHandler.CreateToken(TokenDescriptor);

          return  TokenHandler.WriteToken(Token);
        }

    }
}