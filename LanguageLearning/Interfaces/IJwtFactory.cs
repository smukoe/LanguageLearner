using LanguageLearning.Models;
using LanguageLearning.Models.JWT;
using LanguageLearning.Models.UserAccount;
using LanguageLearning.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Interfaces
{
    public interface IJwtFactory
    {
        JwToken GenerateToken(UserData user);
        JwToken GenerateToken(string subject);
        UserData Authenticate(LoginModel login);        
    }
}
