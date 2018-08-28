using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LanguageLearning.Models;
using System.IO;
using Newtonsoft.Json;
using LanguageLearning.Interfaces;
using LanguageLearning.Models.UserAccount;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace LanguageLearning.Pages
{    
    public class AccountValidation
    {
        public bool IsValid { get; set; }        
    }

    public class UserCreateModel : PageModel
    {                 
        private readonly WordContext _context;
        private readonly IHashing _hashing;
        public UserCreateModel(WordContext context, 
                               IHashing hashing)
        {
            _context = context;
            _hashing = hashing;
        }

        [BindProperty]
        public UserData UserData { get; set; }

        public IActionResult OnGet()
        {
            string cookieValue = ReadCookie("Token");
            if (!String.IsNullOrEmpty(cookieValue))
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }     
            
            HashPassword();
            _context.UserData.Add(UserData);
            await _context.SaveChangesAsync();

            return RedirectToPage("/UserAccount/UserLogin");
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

            UserData.Password = _hashing.GetHash((string)UserData.Password, generatedSalt);
            UserData.StringifiedSalt = Convert.ToBase64String(generatedSalt);
        }
        
        public JsonResult OnGetIsDuplicateUsername(string Username)
        {
            AccountValidation checkUsername = new AccountValidation();
            var allUsers = _context.UserData;
            UserData user = allUsers.FirstOrDefault(u => u.UserName == Username);

            if (user == null)            
                checkUsername.IsValid = true;           
            else           
                checkUsername.IsValid = false;                                                                                   

            return new JsonResult(checkUsername);
        }    
        
        public JsonResult OnGetIsValidPassword(string Password)
        {
            AccountValidation checkPasswordRegex = new AccountValidation();
            string regex = @"^[A-Za-z0-9]+[a-zA-Z0-9]*$";
            
            if (!String.IsNullOrEmpty(Password) && IsRegexValidate(Password, regex))        
                checkPasswordRegex.IsValid = true;            
            else            
                checkPasswordRegex.IsValid = false;

            return new JsonResult(checkPasswordRegex);
        }

        private bool IsRegexValidate(string inputToValidate, string regex)
        {
            Match match = Regex.Match(inputToValidate, regex, RegexOptions.IgnoreCase);
            return match.Success;
        }
    }
}