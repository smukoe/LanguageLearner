using System;
using System.Collections.Generic;
using System.Linq;
using LanguageLearning.Cookies;
using LanguageLearning.Interfaces;
using LanguageLearning.Models;
using LanguageLearning.Models.JWT;
using LanguageLearning.Models.UserAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace LanguageLearning.Pages
{
    public class UserDetailsCheck
    {
        public bool IsDetailsMatch { get; set; }       
    }    

    public class UserLoginModel : PageModel
    {
        private readonly WordContext _context;
        private readonly IJwtFactory _jwtFactory;
        private readonly IHashing _hashing;

        public UserLoginModel(IJwtFactory jwtFactory,
                              WordContext context,
                              IHashing hashing)
        {
            _jwtFactory = jwtFactory;
            _context = context;
            _hashing = hashing;
        }
        
        [BindProperty]
        public LoginModel UserLogin { get; set; }
            
        public IActionResult OnGet()
        {
            string cookieValue = ReadCookie("Token");
            if (!String.IsNullOrEmpty(cookieValue))
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {            
            if (!ModelState.IsValid)
            {
                return Page();
            }
            else
            {
                UserData user = _jwtFactory.Authenticate(UserLogin);

                if (user != null)
                {
                    JwToken token = _jwtFactory.GenerateToken(user);
                    CookieModel cookieModel = new CookieModel
                    {
                        Name = "Token",
                        Value = token.TokenValue
                    };
                    WriteCookie(cookieModel, token.ExpiryTimeInMinutes);

                    return RedirectToPage("/Index");
                }
                else
                    return Page();
            }
        }

        public void WriteCookie(CookieModel cookie, int expiryTimeInMinutes)
        {
            CookieOptions cookieOptions = new CookieOptions
            {
                Expires = DateTime.Now.AddMinutes(expiryTimeInMinutes)
            };

            Response.Cookies.Append(cookie.Name, cookie.Value, cookieOptions);
        }

        public JsonResult OnGetCheckUserLogin(string Username, string Password)
        {
            IQueryable<UserData> UserQuery = from u in _context.UserData
                                             select u;

            List<UserData> LoginDetails = new List<UserData>();
            UserDetailsCheck CheckLogin = new UserDetailsCheck();

            if (!string.IsNullOrEmpty(Username))
            {
                UserQuery = UserQuery.Where(u => u.UserName == Username);
            }
            LoginDetails = UserQuery.ToList();

            CheckLogin.IsDetailsMatch = VerifyPassword(Password, LoginDetails);

            return new JsonResult(CheckLogin);
        }

        private bool VerifyPassword(string password, List<UserData> LoginDetails)
        {
            if ((LoginDetails != null) && (LoginDetails.Any()))
            {
                byte[] userSalt;
                string hashedPassword;

                foreach (UserData userItem in LoginDetails)
                {
                    userSalt = Convert.FromBase64String(userItem.StringifiedSalt);
                    hashedPassword = GetHashedPassword(password, userSalt);

                    if (LoginDetails.Exists(p => p.Password == hashedPassword))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        private string GetHashedPassword(string UserPassword, byte[] UserSalt)
        {
            return _hashing.GetHash(UserPassword, UserSalt);
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