using LanguageLearning.Enums;
using LanguageLearning.Models.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Models.LanguageQuiz.WordAppearances
{
    public class UsersCurrentWord
    {
        public UserData User { get; set; }
        public Language WordLanguage { get; set; }
        public JapaneseWord CurrentJapaneseWord { get; set; }
        public Hiragana CurrentHiragana { get; set; }
        public Katakana CurrentKatakana { get; set; }
        public KoreanWord CurrentKoreanWord { get; set; }
    }
}
