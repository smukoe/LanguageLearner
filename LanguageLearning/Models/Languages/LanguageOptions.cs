using LanguageLearning.Enums;
using LanguageLearning.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Models.Languages
{
    public class LanguageOptions : ILanguageOptions
    {
        public Language MapStringToEnum(string input)
        {
            switch (input)
            {
                case "English":
                    return Language.English;
                case "Japanese":
                    return Language.Japanese;
                case "Korean":
                    return Language.Korean;
                default:
                    return Language.Unrecognised;
            }
        }
    }
}
