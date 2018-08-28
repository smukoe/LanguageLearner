using LanguageLearning.Models.UserAccount;

namespace LanguageLearning.Models.LanguageQuiz.WordAppearances
{
    public class JapaneseWordAppearance
    {
        public int ID { get; set; }
        public UserData User { get; set; }
        public JapaneseWord JapaneseWord { get; set; }       
    }
}
