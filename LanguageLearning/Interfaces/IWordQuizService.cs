using LanguageLearning.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Interfaces
{
    public interface IWordQuizService
    {
        JapaneseWord RandomJapaneseWordSelect();
        Hiragana RandomHiraganaSelect();
    }
}
