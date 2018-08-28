using LanguageLearning.Enums;
using LanguageLearning.Models.UserAccount;

namespace LanguageLearning.Models.LanguageQuiz
{
    public class QuizSessionModel
    {
        public int ID { get; set; }
        public UserData CurrentUser { get; set; }
        public Language CurrentLanguage { get; set; }
        public int WordLimit { get; set; }
        public bool IsPractice { get; set; }
    }
}
