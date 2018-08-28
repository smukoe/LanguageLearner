using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageLearning.Cookies;
using LanguageLearning.Interfaces;
using LanguageLearning.Models;
using LanguageLearning.Models.JWT;
using LanguageLearning.Models.UserAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace LanguageLearning.Pages.UserAccount.Settings
{
    public class ValidatePasswordModel : PageModel
    {
        private readonly IJwtValidation _jwtValidation;
        private readonly IJwtFactory _jwtFactory;
        private readonly ILoginManager _loginManager;
        public ValidatePasswordModel(IJwtValidation jwtValidation, 
                                     IJwtFactory jwtFactory, 
                                     ILoginManager loginManager)
        {
            _jwtValidation = jwtValidation;
            _jwtFactory = jwtFactory;
            _loginManager = loginManager;            
        }

        public UserData UserDetails { get; }

        [BindProperty]
        public LoginModel UserLogin { get; set; }
        
        public IActionResult OnGet(string urlPath)
        {
            string userToken = ReadCookie("Token");
                                    
            if (String.IsNullOrEmpty(userToken))
            {
                return RedirectToPage("/UserAccount/UserLogin");
            }
            else if (_jwtValidation.ValidateToken(userToken))
            {
                CookieModel cookie = new CookieModel
                {
                    Name = "PreviousPage",
                    Value = urlPath
                };
                WriteCookie(cookie, int.MaxValue); //int.MaxValue represents the expiry time

                JObject userClaims = GetUserClaims();
                UserLogin = new LoginModel
                {
                    Username = _jwtValidation.JSONGetValue(userClaims, "sub")
                };
                return Page();
            }
            else
            {
                return BadRequest("Token could not be validated");
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {              
                if (_loginManager.Authenticate(UserLogin))
                {
                    UserData userData = _loginManager.GetUserDetails(UserLogin);
                    string pageUrl = ReadCookie("PreviousPage");

                    if (DoesCookieExist(pageUrl))
                    {                                               
                        JwToken token = _jwtFactory.GenerateToken(pageUrl);
                        int cookieExpiryInMinutes = 5;
                        CookieModel cookieModel = new CookieModel
                        {
                            Name = "PasswordValidation",
                            Value = token.TokenValue
                        };

                        WriteCookie(cookieModel, cookieExpiryInMinutes);
                        return RedirectToPage(pageUrl);                       
                    }                                            
                    else                  
                        return RedirectToPage("/UserAccount/UserAccount");                    
                }                    
                else
                    return Page();
            }
        }

        public JObject GetUserClaims()
        {           
            string userToken = ReadCookie("Token");
            JObject json = _jwtValidation.ParseTokenToJSON(userToken);

            return json;
        }

        public string ReadCookie(string cookieName)
        {
            string cookieValue = Request.Cookies[cookieName];
            if (cookieValue != null)
                return cookieValue;
            else
                return null;
        }

        public void WriteCookie(CookieModel cookie, int expiryTimeInMinutes)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(expiryTimeInMinutes)
            };

            Response.Cookies.Append(cookie.Name, cookie.Value, cookieOptions);
        }

        private bool DoesCookieExist(string cookieValue)
        {
            if (cookieValue != null)
                return true;
            else
                return false;
        }

        public JsonResult OnGetClaimsValidatePassword()
        {         
            string userToken = ReadCookie("Token");

            if (String.IsNullOrEmpty(userToken))
            {
                return new JsonResult("Invalid");
            }
            else
            {
                JObject json = _jwtValidation.ParseTokenToJSON(userToken);
                string username = _jwtValidation.JSONGetValue(json, "sub");
                return new JsonResult(json);
            }
        }
    }
}