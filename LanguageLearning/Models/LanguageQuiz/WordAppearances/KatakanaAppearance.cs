using LanguageLearning.Models.UserAccount;

namespace LanguageLearning.Models.LanguageQuiz.WordAppearances
{
    public class KatakanaAppearance
    {
        public int ID { get; set; }
        public UserData User { get; set; }
        public Katakana Katakana { get; set; }
    }
}
