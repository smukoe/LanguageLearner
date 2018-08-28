using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageLearning.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace LanguageLearning.Pages.UserAccount
{
    public class LogoutModel : PageModel
    {                        
        public IActionResult OnGet()
        {
            string tokenValue = ReadCookie("Token");
            if (tokenValue != null)
            {
                Response.Cookies.Delete("Token");
            }            
            return RedirectToPage("/Index");
        }

        public string ReadCookie(string cookieName)
        {
            string cookieValue = Request.Cookies[cookieName];

            if (cookieValue != null)            
                return cookieValue;
            else
                return null;
        }
    }
}