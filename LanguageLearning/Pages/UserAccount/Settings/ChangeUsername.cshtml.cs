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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace LanguageLearning.Pages.UserAccount.Settings
{
    public class ChangeUsernameModel : PageModel
    {
        private readonly WordContext _context;
        private readonly IJwtFactory _jwtFactory;
        private readonly IJwtValidation _jwtValidation;
        public ChangeUsernameModel(WordContext context, 
                                   IJwtFactory jwtFactory, 
                                   IJwtValidation jwtValidation)
        {
            _context = context;
            _jwtFactory = jwtFactory;
            _jwtValidation = jwtValidation;           
        }

        [BindProperty]
        public UserData UserDetails { get; set; }        
        
        public async Task<IActionResult> OnGetAsync()
        {            
            string passwordToken = ReadCookie("PasswordValidation");
            string userToken = ReadCookie("Token");
            Response.Cookies.Delete("PreviousPage");
            
            if (String.IsNullOrEmpty(userToken))
            {
                return RedirectToPage("/UserAccount/UserLogin");
            }
            else if (_jwtValidation.ValidateToken(passwordToken) && _jwtValidation.ValidateToken(userToken))
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

            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            if (_jwtValidation.ValidateToken(userToken))
            {
                _context.Attach(UserDetails).State = EntityState.Modified;
                try
                {                                                           
                    await _context.SaveChangesAsync();
                    
                    JwToken token = _jwtFactory.GenerateToken(UserDetails);
                    CookieModel cookieModel = CreateCookieModel("Token", token.TokenValue);

                    WriteCookie(cookieModel, token.ExpiryTimeInMinutes);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserDetailExists(UserDetails.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                Response.Cookies.Delete("PasswordValidation");
                return RedirectToPage("/UserAccount/UserAccount");
            }
            else if (!_jwtValidation.ValidateToken(userToken))
            {
                return Unauthorized();
            }
            else
            {
                return Page();
            }            
        }

        private bool UserDetailExists(int id)
        {
            return _context.JapaneseWord.Any(e => e.ID == id);
        }                              
       
        private CookieModel CreateCookieModel(string cookieName, string cookieValue)
        {
            CookieModel cookieModel = new CookieModel
            {
                Name = cookieName,
                Value = cookieValue
            };
            return cookieModel;
        }

        public void WriteCookie(CookieModel cookie, int expiryTimeInMinutes)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(expiryTimeInMinutes)
            };

            Response.Cookies.Append(cookie.Name, cookie.Value, cookieOptions);
        }
        
        public JsonResult OnGetClaimsChangeUsername()
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