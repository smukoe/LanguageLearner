using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LanguageLearning.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using LanguageLearning.Interfaces;
using LanguageLearning.Cookies;
using Newtonsoft.Json.Linq;

namespace LanguageLearning.Pages.Japanese
{
    public class KanaTableModel : PageModel
    {        
        private IJwtValidation JwtValidation { get; set; }
        public KanaTableModel(IJwtValidation jwtValidation)
        {
            JwtValidation = jwtValidation;
        }

        public IActionResult OnGet()
        {            
            string userToken = ReadCookie("Token");

            if(String.IsNullOrEmpty(userToken))
            {
                return RedirectToPage("/UserAccount/UserLogin");
            }           
            else if (JwtValidation.ValidateToken(userToken))
            {
                return Page();                               
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

        public void WriteCookie(CookieModel cookieModel, int expiryTime)
        {
            throw new NotImplementedException();
        }

        public JsonResult OnGetClaimsKanaTable()
        {           
            string userToken = ReadCookie("Token");
            JObject json = JwtValidation.ParseTokenToJSON(userToken);            

            return new JsonResult(json);
        }       
    }   
}