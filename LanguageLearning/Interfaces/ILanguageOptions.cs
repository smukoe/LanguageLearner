using LanguageLearning.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Interfaces
{
    public interface ILanguageOptions
    {
        Language MapStringToEnum(string input);
    }
}
