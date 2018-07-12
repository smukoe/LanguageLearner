using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageLearning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LanguageLearning.Pages
{
    public class UserDetailsCheck
    {
        public bool IsDetailsMatch { get; set; }
    }

    public class UserLoginModel : PageModel
    {
        private readonly WordContext _context;

        public UserLoginModel(WordContext context)
        {
            _context = context;
        }
       
        public void OnGet()
        {
            
        }

        //GET request for login
        public JsonResult OnGetCheckUserLogin(string Username, string Password)
        {
            IQueryable<UserData> UserQuery = from u in _context.UserData
                                                   select u;

            UserDetailsCheck CheckLogin = new UserDetailsCheck();
            List<UserData> LoginDetails = new List<UserData>();
            LoginDetails.Clear();

            if (!string.IsNullOrEmpty(Username))
            {
                //Checks if the entered username exists in the database
                UserQuery = UserQuery.Where(u => u.UserName == Username);              
            }
            LoginDetails = UserQuery.ToList();
                       
            if((LoginDetails != null) && (LoginDetails.Any())) //Check for null just in case, and if the list contains an element it means that a username exists
            {
                byte[] UserSalt;
                string HashedPassword;

                //Checks through each item in the list (should be one)
                foreach (UserData userItem in LoginDetails)
                {
                    //Retrieve the associated salt and get the hashed password
                    UserSalt = Convert.FromBase64String(userItem.Salt);
                    HashedPassword = GetHashedPassword(Password, UserSalt);

                    //Checking if the passwords match
                    if (LoginDetails.Exists(p => p.Password == HashedPassword))
                    {
                        CheckLogin.IsDetailsMatch = true;
                    }
                    else
                    {
                        CheckLogin.IsDetailsMatch = false;
                    }
                }    
            }
            else
            {
                CheckLogin.IsDetailsMatch = false;
            }

            return new JsonResult(CheckLogin);
        }

        public string GetHashedPassword(string UserPassword, byte[] UserSalt)
        {
            Hashing HashUser = new Hashing();
            return HashUser.GetHash(UserPassword, UserSalt);           
        }
    }  
}