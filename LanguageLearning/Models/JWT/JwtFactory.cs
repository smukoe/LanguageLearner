using LanguageLearning.Interfaces;
using LanguageLearning.Models.UserAccount;
using LanguageLearning.Pages;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LanguageLearning.Models.JWT
{
    public class JwtFactory : IJwtFactory
    {
        private readonly IConfiguration _config;
        private readonly ILoginManager _loginManager;
        public JwtFactory(IConfiguration config, 
                          ILoginManager loginManager)
        {
            _config = config;            
            _loginManager = loginManager;
        }

        
        public JwToken GenerateToken(UserData user)
        {            
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),           
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            int tokenExpiryInMinutes = 30;

            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Audience"],
              claims:  claims,
              expires: DateTime.Now.AddMinutes(tokenExpiryInMinutes),
              signingCredentials: creds);
            
            JwToken jwToken = new JwToken
            {
                TokenValue = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiryTimeInMinutes = tokenExpiryInMinutes
            };
            return jwToken;
        }

        public JwToken GenerateToken(string subject)
        {
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, subject),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            int tokenExpiryInMinutes = 15;

            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Audience"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(tokenExpiryInMinutes),
              signingCredentials: creds);

            JwToken jwToken = new JwToken
            {
                TokenValue = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiryTimeInMinutes = tokenExpiryInMinutes
            };
            return jwToken;
        }

        public UserData Authenticate(LoginModel login)
        {
            UserData user = null;           

            if (_loginManager.FindByUsername(login) && _loginManager.PasswordSignIn(login))            
                return user = _loginManager.GetUserDetails(login);
            else
                return user;
        }
    }
}
