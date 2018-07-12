using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Models
{
    public static class CaseIgnore
    {
        public static bool CaseInsensitiveContains(this string wordToTranslate, string value, StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        //Method used to ignore case when comparing strings
        {
            return wordToTranslate.IndexOf(value, stringComparison) >= 0;
        }
    }
}
