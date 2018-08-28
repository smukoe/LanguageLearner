using LanguageLearning.Models;
using LanguageLearning.Models.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Interfaces
{
    public interface ILoginManager
    {
        bool FindByUsername(LoginModel loginDetails);
        bool PasswordSignIn(LoginModel login);       
        UserData GetUserDetails(LoginModel login);         
        bool Authenticate(LoginModel userLogin);
    }
}
