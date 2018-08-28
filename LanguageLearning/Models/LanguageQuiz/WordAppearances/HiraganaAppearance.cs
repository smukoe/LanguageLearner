using LanguageLearning.Models.UserAccount;

namespace LanguageLearning.Models.LanguageQuiz.WordAppearances
{
    public class HiraganaAppearance
    {
        public int ID { get; set; }
        public UserData User { get; set; }
        public Hiragana Hiragana { get; set; }        
    }
}
