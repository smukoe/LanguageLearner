using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageLearning.Cookies;
using LanguageLearning.Interfaces;
using LanguageLearning.Models;
using LanguageLearning.Models.JWT;
using LanguageLearning.Models.UserAccount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;

namespace LanguageLearning.Pages.UserAccount.Settings
{
    public class PasswordValid
    {
        public bool IsValid { get; set; }
    }   

    public class ChangePasswordModel : PageModel
    {
        private readonly IJwtValidation _jwtValidation;
        private readonly WordContext _context;
        private readonly IHashing _hashing;
        public ChangePasswordModel(IJwtValidation jwtValidation, 
                                   WordContext context,
                                   IHashing hashing)
        {
            _jwtValidation = jwtValidation;
            _context = context;
            _hashing = hashing;
        }

        [BindProperty]
        public UserData UserDetails { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {           
            string tempToken = ReadCookie("ChangePasswordTemp");
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
            string tempToken = ReadCookie("ChangePasswordTemp");
            string userToken = ReadCookie("Token");
            Response.Cookies.Delete("PreviousPage");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            HashPassword();

            if (_jwtValidation.ValidateToken(userToken) && _jwtValidation.ValidateToken(tempToken))
            {
                _context.Attach(UserDetails).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync();                    
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
                Response.Cookies.Delete("ChangePasswordTemp");
                return RedirectToPage("/UserAccount/Logout");
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
            return _context.UserData.Any(e => e.ID == id);
        }

        public string ReadCookie(string cookieName)
        {
            string cookieValue = Request.Cookies[cookieName];

            if (cookieValue != null)
                return cookieValue;
            else
                return null;
        }

        private void HashPassword()
        {           
            byte[] generatedSalt = _hashing.GetSalt();
            UserDetails.Password = _hashing.GetHash(UserDetails.Password, generatedSalt);
            UserDetails.StringifiedSalt = Convert.ToBase64String(generatedSalt);
        }

        public JsonResult OnGetClaimsChangePassword()
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

        public JsonResult OnGetValidateOldPassword(string password)
        {            
            PasswordValid passwordValid = new PasswordValid();
            if (CheckCorrectPassword(password))                           
                passwordValid.IsValid = true;            
            else           
                passwordValid.IsValid = false;     
            
            return new JsonResult(passwordValid);
        }        

        private bool CheckCorrectPassword(string password)
        {            
            string userToken = ReadCookie("Token");
            JObject json = _jwtValidation.ParseTokenToJSON(userToken);
            string username = _jwtValidation.JSONGetValue(json, "sub");

            LoginManager loginManager = new LoginManager(_context);

            LoginModel loginModel = new LoginModel
            {
                Username = username,
                Password = password
            };

            if (loginManager.PasswordSignIn(loginModel))
                return true;
            else
                return false;
        }
    }
}