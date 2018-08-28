using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageLearning.Interfaces;
using LanguageLearning.Models;
using LanguageLearning.Models.UserAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace LanguageLearning.Pages.UserAccount
{
    public class UserDetailsDisplay
    {
        public string Username { get; set; }
    }

    public class UserAccountModel : PageModel
    {
        private IJwtValidation _jwtValidation;
        public UserAccountModel(IJwtValidation jwtValidation)
        {
            _jwtValidation = jwtValidation;
        }

        public string ChangeUsernameUrl = "/UserAccount/Settings/ChangeUsername";
        public string ChangePasswordUrl = "/UserAccount/Settings/ChangePassword";
        public string DeleteAccountUrl = "/UserAccount/Settings/DeleteAccount";

        public AccountDisplay UserAccount { get; set; }
        public UserDetailsDisplay UserDetails { get; set; }
        
        public IActionResult OnGet()
        {                      
            string userToken = ReadCookie("Token");

            if (String.IsNullOrEmpty(userToken))
            {
                return RedirectToPage("/UserAccount/UserLogin");
            }
            else if (_jwtValidation.ValidateToken(userToken))
            {
                UserAccount = new AccountDisplay();
                UserAccount = GetAccountDetails();
                if (UserAccount.Username == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Page();
                }               
            }
            else
            {
                return BadRequest("Token could not be validated");
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

        private AccountDisplay GetAccountDetails()
        {
            AccountDisplay accountDisplay = new AccountDisplay();            
            string userToken = ReadCookie("Token");

            if (String.IsNullOrEmpty(userToken))
            {
                accountDisplay.Username = null;
            }
            else
            {
                JObject json = _jwtValidation.ParseTokenToJSON(userToken);
                accountDisplay.Username = _jwtValidation.JSONGetValue(json, "sub");               
            }
            return accountDisplay;
        }

        public JsonResult OnGetClaimsUserAccount()
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