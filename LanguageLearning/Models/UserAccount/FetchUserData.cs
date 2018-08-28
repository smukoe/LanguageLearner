using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Models.UserAccount
{
    public class FetchUserData
    {
        private readonly WordContext _context;

        public FetchUserData(WordContext context)
        {
            _context = context;
        }

        public UserData GetUserDataFromUsername(string username)
        {
            var allUsers = _context.UserData;
            return allUsers.FirstOrDefault(u => u.UserName == username);
        }
    }
}
