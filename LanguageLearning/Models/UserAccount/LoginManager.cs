using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Models.UserAccount
{
    public class LoginManager 
    {
        private WordContext _context;       

        private IQueryable<UserData> UserQuery;

        public LoginManager(WordContext context)
        {
            _context = context;           
        }
        
        public bool FindByUsername(LoginModel loginDetails)
        {
            bool isMatch = false;
            List<UserData> listOfUsers = CheckUsername(loginDetails.Username);
            if ((listOfUsers != null) && (listOfUsers.Any()))
            {
                return isMatch = true;
            }
            return isMatch;
        }

        public bool PasswordSignIn(LoginModel login)
        {
            bool isMatch = false;
            List<UserData> listOfUsers = CheckUsername(login.Username);
            Hashing hashing = new Hashing();
            if ((listOfUsers != null) && (listOfUsers.Any()))
            {
                byte[] UserSalt;
                string HashedPassword;

                foreach (UserData userItem in listOfUsers)
                {
                    UserSalt = ConvertSaltToByteArray(userItem.StringifiedSalt);
                    HashedPassword = hashing.GetHash(login.Password, UserSalt);

                    if (listOfUsers.Exists(p => p.Password == HashedPassword))
                    {
                        return isMatch = true;
                    }
                    else
                    {
                        return isMatch = false;
                    }
                }
            }
            return isMatch;
        }

        public UserData GetUserDetails(LoginModel login)
        {
            QueryUserData();
            UserData userData = UserQuery.FirstOrDefault(u => u.UserName == login.Username);
            return userData;
        }

        private IQueryable<UserData> QueryUserData()
        {
            return UserQuery = from u in _context.UserData
                               select u;
        }

        private List<UserData> CheckUsername(string username)
        {
            QueryUserData();
            List<UserData> userLoginDetails = new List<UserData>();
            UserQuery = UserQuery.Where(u => u.UserName == username);
            return userLoginDetails = UserQuery.ToList();
        }       

        private byte[] ConvertSaltToByteArray(string salt)
        {
            return Convert.FromBase64String(salt);
        }
    }
}
