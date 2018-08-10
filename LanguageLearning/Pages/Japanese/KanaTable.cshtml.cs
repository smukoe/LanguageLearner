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

namespace LanguageLearning.Pages.Japanese
{
    public class KanaTableModel : PageModel, ICookieReadAndWrite
    {
        private IConfiguration _config;

        public KanaTableModel(IConfiguration config)
        {
            _config = config;
        }

        public IActionResult OnGet()
        {
            string userToken = ReadCookie("Token");
            if(userToken == null)
            {
                return RedirectToPage("/UserAccount/UserLogin");
            }
            else
            {
                JwtValidation jwtValidation = new JwtValidation(_config);
                if (jwtValidation.ValidateToken(userToken))
                {
                    return Page();
                }
                else
                {
                    return BadRequest("Token could not be validated");
                }
            }                                   
        }

        public string ReadCookie(string cookieName)
        {
            string cookieValue = Request.Cookies[cookieName];
            if (cookieValue != null)
            {
                return cookieValue;
            }
            return null;
        }

        public void WriteCookie(CookieModel cookieModel, int expiryTime)
        {
            throw new NotImplementedException();
        }
    }
}