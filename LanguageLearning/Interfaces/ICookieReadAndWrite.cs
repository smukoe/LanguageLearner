using LanguageLearning.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Interfaces
{
    interface ICookieReadAndWrite
    {
        void WriteCookie(CookieModel cookieModel, int expiryTime);
        string ReadCookie(string cookieName);
    }
}
