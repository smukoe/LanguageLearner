using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using System.Text;
using Newtonsoft.Json.Linq;
using LanguageLearning.Interfaces;
using Newtonsoft.Json;

namespace LanguageLearning.Models
{
    public class JwtValidation : IJwtValidation
    {
        private readonly IConfiguration _config;
        public JwtValidation(IConfiguration config)
        {
            _config = config;
        }
        
        public bool ValidateToken(string token)
        {            
            var tokenHandler = new JwtSecurityTokenHandler();
            
            try
            {
                TokenValidationParameters validationParameters = SetupTokenValidationParameters();
                tokenHandler.ValidateToken(token, validationParameters, out var rawJwToken);
                return true;
            }
            catch (SecurityTokenNoExpirationException)
            {
                throw new SecurityTokenNoExpirationException("Token does not have an expiry time");
            }
            catch (Exception)
            {
                return false;
            }
        }

        private TokenValidationParameters SetupTokenValidationParameters()
        {
            var validationParameters = new TokenValidationParameters
            {
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"])),
                ValidateLifetime = true
            };
            return validationParameters;
        }

        public JObject ParseTokenToJSON(string token)
        {            
            string[] tokenParts = token.Split('.');
            string tokenPayload = tokenParts[1];
            tokenPayload = PadToBase64(tokenPayload);                                    
            byte[] encodedTokenBytes = Convert.FromBase64String(tokenPayload);
            string stringifiedPart = Encoding.UTF8.GetString(encodedTokenBytes);
            try
            {
                JObject json = JObject.Parse(stringifiedPart);
                return json;
            }
            catch (JsonReaderException)
            {
                throw new JsonReaderException("Token is invalid");
            }           
        }

        private string PadToBase64(string toBePadded)
        {
            string padding = "=";
            while (toBePadded.Length % 4 != 0)
            {
                toBePadded += padding;
            }
            return toBePadded;
        }

        public string JSONGetValue(JObject jwt, string key)
        {
            string claimValue = jwt[key].Value<string>();
            return claimValue;
        }
    }
}

