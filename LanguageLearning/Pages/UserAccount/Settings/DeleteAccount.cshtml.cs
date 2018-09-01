using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageLearning.Interfaces;
using LanguageLearning.Models;
using LanguageLearning.Models.UserAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace LanguageLearning.Pages.UserAccount.Settings
{
    public class DeleteAccountModel : PageModel
    {
        private readonly IJwtValidation _jwtValidation;
        private readonly WordContext _context;

        public DeleteAccountModel(IJwtValidation jwtValidation, WordContext context)
        {
            _jwtValidation = jwtValidation;
            _context = context;
        }

        [BindProperty]
        public UserData UserDetails { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string tempToken = ReadCookie("PasswordValidation");
            string userToken = ReadCookie("Token");
            Response.Cookies.Delete("PreviousPage");

            if (String.IsNullOrEmpty(userToken))
            {
                return RedirectToPage("/UserAccount/UserLogin");
            }
            else if (_jwtValidation.ValidateToken(tempToken) && _jwtValidation.ValidateToken(userToken))
            {
                JObject json = _jwtValidation.ParseTokenToJSON(userToken);
                string username = _jwtValidation.JSONGetValue(json, "sub");

                UserDetails = await _context.UserData.SingleOrDefaultAsync(m => m.UserName == username);

                if (UserDetails == null)
                {
                    return NotFound();
                }
                return Page();
            }
            else
            {
                return Unauthorized();
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            string userToken = ReadCookie("Token");
            JObject json = _jwtValidation.ParseTokenToJSON(userToken);
            string username = _jwtValidation.JSONGetValue(json, "sub");

            if (_jwtValidation.ValidateToken(userToken))
            {
                UserDetails = await _context.UserData.SingleOrDefaultAsync(m => m.UserName == username);

                if (UserDetails != null)
                {
                    _context.UserData.Remove(UserDetails);
                    await _context.SaveChangesAsync();
                    Response.Cookies.Delete("PasswordValidation");
                    return RedirectToPage("/UserAccount/Logout");
                }
                else
                {
                    return RedirectToPage("/Index");
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
    }
}