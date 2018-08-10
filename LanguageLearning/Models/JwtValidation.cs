using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace LanguageLearning.Models
{
    public class JwtValidation
    {
        private readonly IConfiguration _config;

        public JwtValidation(IConfiguration config)
        {
            _config = config;
        }
        
        public bool ValidateToken(string token)
        {            
            var tokenHandler = new JwtSecurityTokenHandler();
            var secToken = tokenHandler.ReadToken(token);

            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                ValidateLifetime = true
            };

            try
            {
                tokenHandler.ValidateToken(token, validationParameters, out var rawToken);
                return true; 
            }
            catch(Exception)
            {
                return false;
            }            
        }      
    }
}

