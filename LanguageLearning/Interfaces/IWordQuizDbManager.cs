using LanguageLearning.Models.LanguageQuiz;
using LanguageLearning.Models.LanguageQuiz.WordAppearances;
using LanguageLearning.Models.UserAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Interfaces
{
    public interface IWordQuizDbManager
    {
        void AddNewSession(QuizSessionModel quizSessionModel);
        void EndSession(UserData user);
        bool IsExistingSession(UserData user);
        QuizSessionModel GetUsersSession(UserData user);
        bool IsDuplicateWord(UsersCurrentWord usersCurrentWord);
        void UpdateWordAppearance(UsersCurrentWord usersCurrentWord);
        void ResetWordAppearance(UsersCurrentWord usersCurrentWord);
    }
}
