using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using LanguageLearning.Cookies;
using LanguageLearning.Interfaces;
using LanguageLearning.Models;
using LanguageLearning.Models.JWT;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace LanguageLearning.Pages.UserAccount.Settings
{
    public class CreateTemporaryTokenModel : PageModel
    {        
        public IJwtValidation _jwtValidation { get; }
        public IJwtFactory _jwtFactory { get; }
        public CreateTemporaryTokenModel(IJwtValidation jwtValidation, IJwtFactory jwtFactory)
        {
            _jwtValidation = jwtValidation;
            _jwtFactory = jwtFactory;
        }

        public IActionResult OnGet(string urlPath)
        {           
            string userToken = ReadCookie("Token");
            Response.Cookies.Delete("PreviousPage");

            if (String.IsNullOrEmpty(userToken))
            {
                return RedirectToPage("/UserAccount/UserLogin");
            }
            else if (_jwtValidation.ValidateToken(userToken))
            {                
                string randomString = GenerateRandomString();
                JwToken token = _jwtFactory.GenerateToken(randomString);

                try
                {
                    string tokenName = GetTokenNameFromUrlPath(urlPath);

                    SetTokenToCookie(tokenName, token.TokenValue);
                    return RedirectToPage(urlPath);
                }
                catch (ArgumentNullException)
                {
                    return BadRequest();
                }                               
            }
            else
            {
                return Unauthorized();
            }
        }

        public string ReadCookie(string cookieName)
        {
            string cookieValue = Request.Cookies[cookieName];

            if (cookieValue != null)
                return cookieValue;
            else
                return null;
        }

        private string GenerateRandomString()
        {
            byte[] byteArray = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(byteArray);
            }

            return Convert.ToBase64String(byteArray);
        }

        private string GetTokenNameFromUrlPath(string urlPath)
        {
            string tokenName;
            switch (urlPath)
            {
                case "/UserAccount/Settings/ChangePassword":
                    tokenName = "ChangePasswordTemp";
                    break;
                default:
                    tokenName = null;
                    break;
            }
            return tokenName;
        }        

        private void SetTokenToCookie(string tokenName, string tokenValue)
        {
            CookieModel cookie = new CookieModel
            {
                Name = tokenName,
                Value = tokenValue
            };

            WriteCookie(cookie, 5);
        }

        public void WriteCookie(CookieModel cookie, int expiryTimeInMinutes)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(expiryTimeInMinutes)
            };
            Response.Cookies.Append(cookie.Name, cookie.Value, cookieOptions);
        }

        
    }
}