using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Interfaces
{
    public interface IJwtValidation
    {
        bool ValidateToken(string token);
        JObject ParseTokenToJSON(string token);
        string JSONGetValue(JObject jwt, string key);
    }
}
