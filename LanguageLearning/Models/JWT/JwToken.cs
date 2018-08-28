using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Models.JWT
{
    public class JwToken
    {
        public string TokenValue { get; set; }
        public int ExpiryTimeInMinutes { get; set; }
    }
}
