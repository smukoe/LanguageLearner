using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using LanguageLearning.Cookies;
using LanguageLearning.Models;
using LanguageLearning.Models.UserAccount;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LanguageLearning.Pages
{
    public class UserDetailsCheck
    {
        public bool IsDetailsMatch { get; set; }       
    }

    public class JwToken
    {
        public string TokenValue { get; set; }
        public int ExpiryTimeInMinutes { get; set; }
    }

    public class UserLoginModel : PageModel
    {
        
        private readonly WordContext _context;
        private readonly IConfiguration _config;        
        
        public UserLoginModel(WordContext context,
                              IConfiguration config)
        {
            _context = context;
            _config = config;                     
        }
       
        [BindProperty]
        public LoginModel UserLogin { get; set; }

        public IActionResult OnGet()
        {
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
                IActionResult response = Unauthorized();
                var user = Authenticate(UserLogin);

                if (user != null)
                {
                    JwToken token = GenerateToken(user);                    
                    response = RedirectToPage("");                    
                    CookieModel cookieModel = new CookieModel
                    {
                        Name = "Token",
                        Value = token.TokenValue
                    };
                    WriteCookie(cookieModel, token.ExpiryTimeInMinutes);
                }
                return response;
            }
        }       

        private JwToken GenerateToken(UserData user)
        {            
            var claims = new[] {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int tokenExpiryInMinutes = 30;

            var token = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Audience"],
              expires: DateTime.Now.AddMinutes(tokenExpiryInMinutes),
              signingCredentials: creds);
           
            JwToken jwToken = new JwToken
            {
                TokenValue = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiryTimeInMinutes = tokenExpiryInMinutes
            };
            return jwToken;
        }

        private UserData Authenticate(LoginModel login)
        {
            UserData user = null; 

            LoginManager loginManager = new LoginManager(_context);
            if (loginManager.FindByUsername(login) && loginManager.PasswordSignIn(login))
            {
                return user = loginManager.GetUserDetails(login);
            }
            return user;
        }

        private void WriteCookie(CookieModel cookie, int expiryTimeInMinutes)
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
            Hashing HashUser = new Hashing();
            return HashUser.GetHash(UserPassword, UserSalt);           
        }
    }  
}