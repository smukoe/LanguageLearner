using LanguageLearning.Models.UserAccount;

namespace LanguageLearning.Models.LanguageQuiz.WordAppearances
{
    public class KoreanWordAppearance
    {
        public int ID { get; set; }
        public UserData User { get; set; }
        public KoreanWord KoreanWord { get; set; }
    }
}
