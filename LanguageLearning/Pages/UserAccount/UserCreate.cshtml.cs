using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LanguageLearning.Models;
using System.IO;
using Newtonsoft.Json;

namespace LanguageLearning.Pages
{
    public class ExistingAccount
    {
        public bool IsDuplicate { get; set; }
        
    }

    public class UserCreateModel : PageModel
    {                 
        private readonly WordContext _context;

        public UserCreateModel(WordContext context)
        {
            _context = context;
        }

        [BindProperty]
        public UserData UserData { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }     
            
            HashPassword();            
            _context.UserData.Add(UserData);
            await _context.SaveChangesAsync();

            return RedirectToPage("./HooraySuccess");
        }

        public void HashPassword()
        {
            Hashing HashUser = new Hashing();
            HashUser.GeneratedSalt = HashUser.GetSalt();
            
            UserData.Password = HashUser.GetHash(UserData.Password, HashUser.GeneratedSalt);
            UserData.StringifiedSalt = Convert.ToBase64String(HashUser.GeneratedSalt);
        }

        //OnGet to check if username already exists in the database
        public JsonResult OnGetCheckUsername(string Username)
        {
            IQueryable<UserData> UserDetailQuery = from u in _context.UserData
                                                   select u;

            List<UserData> ExistingUsername = new List<UserData>();
            ExistingUsername.Clear();

            if (!string.IsNullOrEmpty(Username))
            {
                //Looks for duplicate username
                UserDetailQuery = UserDetailQuery.Where(user => user.UserName == Username);
            }           
            ExistingUsername = UserDetailQuery.ToList();

            ExistingAccount CheckDuplicateUsername = new ExistingAccount();            
            if ((ExistingUsername != null) && (ExistingUsername.Any()))
            {
                CheckDuplicateUsername.IsDuplicate = true;
            }
            else
            {
                CheckDuplicateUsername.IsDuplicate = false;
            }

            return new JsonResult(CheckDuplicateUsername);
        }                
    }
}