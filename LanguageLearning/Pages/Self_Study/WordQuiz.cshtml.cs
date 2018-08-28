using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LanguageLearning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;
using Newtonsoft.Json;
using LanguageLearning.Models.Languages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using LanguageLearning.Interfaces;
using LanguageLearning.Models.UserAccount;
using LanguageLearning.Models.LanguageQuiz;
using LanguageLearning.Models.LanguageQuiz.WordAppearances;
using LanguageLearning.Enums;

namespace LanguageLearning.Pages.Self_Study
{       
    public class GameResult
    {
        public bool AnswerCheck { get; set; }
    }   

    public class GameEndState
    {
        public bool IsFinished { get; set; }
    }   

    public class WordCount
    {
        public int AllWordsCount { get; set; }
        public int WordAppearanceCount { get; set; }
    }   

    public class WordQuizModel : PageModel
    {
        private readonly WordContext _context;
        private readonly IJwtValidation _jwtValidation;
        private readonly IWordQuizService _wordQuizService;
        private readonly IWordQuizDbManager _wordQuizDbManager;
        private readonly ILanguageOptions _languageOptions;

        public WordQuizModel(WordContext context, 
                             IJwtValidation jwtValidation, 
                             IWordQuizService wordQuizService,
                             IWordQuizDbManager wordQuizDbManager,
                             ILanguageOptions languageOptions)
        {          
            _context = context;
            _jwtValidation = jwtValidation;
            _wordQuizService = wordQuizService;
            _wordQuizDbManager = wordQuizDbManager;
            _languageOptions = languageOptions;
        }

        public int AllWordsCount { get; set; }
        public int WordAppearanceCount { get; set; }

        //Variables for random word selection
        public JapaneseWord RandomJapWord { get; private set; }
        public Hiragana RandomHiragana { get; set; }

        //Variables for memorise game
        public bool IsPractice { get; set; }
        public List<Hiragana> HiraganaAppearance = new List<Hiragana>();
        public List<Hiragana> IncorrectHiraganaAnswers = new List<Hiragana>();

        //Variables for Word translate game       
        public JapaneseWord CorrectJWord { get; private set; }
        public Hiragana CorrectHiragana { get; private set; }

        public IActionResult OnGet()
        {
            string userToken = ReadCookie("Token");

            if (String.IsNullOrEmpty(userToken))
            {
                return RedirectToPage("/UserAccount/UserLogin");
            }
            else if (_jwtValidation.ValidateToken(userToken))
            {
                return Page();
            }
            else
            {
                return BadRequest("Token could not be validated");
            }
        }

        public JsonResult OnGetClaimsWordQuiz()
        {
            string userToken = ReadCookie("Token");

            if (String.IsNullOrEmpty(userToken))
            {
                return new JsonResult("Invalid");
            }
            else
            {
                JObject tokenJSON = _jwtValidation.ParseTokenToJSON(userToken);
                string username = _jwtValidation.JSONGetValue(tokenJSON, "sub");
                return new JsonResult(tokenJSON);
            }
        }       

        public JsonResult OnGetSetGameType(string mainLanguage, bool isPractice, int wordLimit)
        {            
            WordCount wordCount = new WordCount();

            string userToken = ReadCookie("Token");
            
            if (_jwtValidation.ValidateToken(userToken))
            {
                JObject tokenJSON = _jwtValidation.ParseTokenToJSON(userToken);
                string username = _jwtValidation.JSONGetValue(tokenJSON, "sub");
                UserData user = GetUserData(username);
                if (_wordQuizDbManager.IsExistingSession(user))
                {
                    return new JsonResult("Session Exists");
                }
                else
                {
                    QuizSessionModel quizSessionModel = new QuizSessionModel
                    {
                        CurrentUser = user,
                        CurrentLanguage = _languageOptions.MapStringToEnum(mainLanguage),
                        WordLimit = wordLimit,
                        IsPractice = isPractice
                    };

                    _wordQuizDbManager.AddNewSession(quizSessionModel);
                }
                return new JsonResult("done");
            }            
            else
            {                
                return new JsonResult("Invalid");
            }            
        }

        public JsonResult OnGetCheckGameStatus(int TotalWordCount)
        {
            GameEndState Game = new GameEndState();
            string userToken = ReadCookie("Token");
            int wordLimit = 0;

            if (String.IsNullOrEmpty(userToken))
            {
                //Force finish the game
                Game.IsFinished = true;
                return new JsonResult(Game);
            }
            else if (_jwtValidation.ValidateToken(userToken))
            {
                JObject tokenJSON = _jwtValidation.ParseTokenToJSON(userToken);
                string username = _jwtValidation.JSONGetValue(tokenJSON, "sub");
                UserData user = GetUserData(username);
                QuizSessionModel usersSessions = _wordQuizDbManager.GetUsersSession(user);
                wordLimit = usersSessions.WordLimit;
            }
            else
            {
                //Force finish the game
                Game.IsFinished = true;
                return new JsonResult(Game);
            }
                                                
            if (TotalWordCount != 0 && TotalWordCount == wordLimit)
            {
                Game.IsFinished = true;
                
            }
            else
            {
                Game.IsFinished = false;
            }
            return new JsonResult(Game);
        }             

        private UserData GetUserData(string username)
        {
            var allUsers = _context.UserData;
            UserData userData = allUsers.FirstOrDefault(u => u.UserName == username);

            return userData;
        }

        public JsonResult OnGetJapanWord()
        {
            JapaneseWord randomJapaneseWord = new JapaneseWord();
            List<JapaneseWord> randomJWords = new List<JapaneseWord>();
            int maxWordDisplay = 4;

            string userToken = ReadCookie("Token");           
            
            if (_jwtValidation.ValidateToken(userToken))
            {
                JObject tokenJSON = _jwtValidation.ParseTokenToJSON(userToken);
                string username = _jwtValidation.JSONGetValue(tokenJSON, "sub");
                UserData user = GetUserData(username);

                while (randomJWords.Count < maxWordDisplay)
                {
                    randomJapaneseWord = _wordQuizService.RandomJapaneseWordSelect();                                 
                    UsersCurrentWord usersCurrentWord = new UsersCurrentWord
                    {
                        User = user,
                        CurrentJapaneseWord = randomJapaneseWord,
                        WordLanguage = Language.Japanese
                    };

                    if (!randomJWords.Any() && !_wordQuizDbManager.IsDuplicateWord(usersCurrentWord))
                    {
                        randomJWords.Add(randomJapaneseWord);
                    }
                    else if (!randomJWords.Contains(randomJapaneseWord))
                    {
                        randomJWords.Add(randomJapaneseWord);
                    }
                }                
            }
            else
            {
                //Return invalid result              
                return new JsonResult("Invalid");
            }                                                                       
            return new JsonResult(randomJWords);             
        }       

        public JsonResult OnGetHiragana()
        {            
            List<Hiragana> randomHiraganaWords = new List<Hiragana>();
            int maxWordDisplay = 4;

            if (HiraganaAppearance.Safe().Any())
            {
                WordAppearanceCount = HiraganaAppearance.Count();
            }
            else
            {
                WordAppearanceCount = 0;
            }
            
            while (randomHiraganaWords.Count < maxWordDisplay)
            {
                RandomHiragana = _wordQuizService.RandomHiraganaSelect();
                if (!randomHiraganaWords.Any() && !CheckHiraganaOccurance(RandomHiragana)) //For the first word added to the list because it is the display word
                {
                    randomHiraganaWords.Add(RandomHiragana);
                }
                else if (!randomHiraganaWords.Contains(RandomHiragana)) //All other words
                {
                    randomHiraganaWords.Add(RandomHiragana);
                }
            }
            return new JsonResult(randomHiraganaWords);
        }

        public JsonResult OnGetCheckAnswerJapanese(string Question, string Answer, string TranslateFrom)
        {            
            var AllWords = _context.JapaneseWord;
            GameResult IsTheAnswerCorrect = new GameResult();

            if (TranslateFrom == "Japanese")
            {
                foreach (JapaneseWord currentWord in AllWords)
                {
                    if (currentWord.Name.Equals(Question))
                    {
                        CorrectJWord = currentWord;
                    }
                }

                if (CorrectJWord.Definition.Equals(Answer))
                {
                    IsTheAnswerCorrect.AnswerCheck = true;
                }
                else
                {
                    IsTheAnswerCorrect.AnswerCheck = false;
                }
            }
            else if (TranslateFrom == "English")
            {
                foreach (JapaneseWord currentWord in AllWords)
                {
                    if (currentWord.Definition.Equals(Question))
                    {
                        CorrectJWord = currentWord;
                    }
                }
                if (CorrectJWord.Name.Contains(Answer))
                {
                    IsTheAnswerCorrect.AnswerCheck = true;
                }
                else
                {
                    IsTheAnswerCorrect.AnswerCheck = false;
                }
            }
                                      
            return new JsonResult(IsTheAnswerCorrect);
        }       

        public JsonResult OnGetCheckAnswerHiragana(string Question, string Answer, string TranslateFrom)
        {
            var AllWords = _context.Hiragana;
            
            GameResult IsTheAnswerCorrect = new GameResult();

            if (TranslateFrom == "Hiragana")
            {
                //Find the object corresponding to the question
                foreach (Hiragana currentWord in AllWords)
                {
                    if (currentWord.Kana.Equals(Question))
                    {
                        CorrectHiragana = currentWord;
                    }
                }
               
                if (CorrectHiragana.Romaji.Equals(Answer))
                {
                    IsTheAnswerCorrect.AnswerCheck = true;
                    HiraganaAddToList(true, CorrectHiragana);                             
                }
                else
                {
                    IsTheAnswerCorrect.AnswerCheck = false;
                    HiraganaAddToList(false, CorrectHiragana);                   
                }
            }
            else if (TranslateFrom == "English")
            {
                foreach (Hiragana currentWord in AllWords)
                {
                    if (currentWord.Romaji.Equals(Question))
                    {
                        CorrectHiragana = currentWord;
                    }
                }

                if (CorrectHiragana.Kana.Equals(Answer))
                {
                    IsTheAnswerCorrect.AnswerCheck = true;
                }
                else
                {
                    IsTheAnswerCorrect.AnswerCheck = false;
                }
            }
           
            return new JsonResult(IsTheAnswerCorrect);
        }

        public bool CheckHiraganaOccurance(Hiragana HiraganaToCheck)
        {
            //Checks if the hiragana has been selected before
            bool IsExist = false;
            if (!IsPractice && HiraganaAppearance.Safe().Any())
            {
                foreach (Hiragana currentHiragana in HiraganaAppearance)
                {
                    if (currentHiragana.Equals(HiraganaToCheck))
                    {
                        IsExist = true;
                        break;
                    }
                    else
                    {                      
                        IsExist = false;
                    }
                }                       
            }            
            return IsExist;
        }

        public void HiraganaAddToList(bool IsCorrect, Hiragana hiragana)
        {
            bool IsExists = CheckHiraganaOccurance(hiragana);

            //Adds the word to a list if it has been answered incorrectly  
            if (!IsPractice && !IsCorrect)
            {
                if (IncorrectHiraganaAnswers.Any())
                {
                    bool IsExist = false;
                    foreach (Hiragana currentHiragana in IncorrectHiraganaAnswers)
                    {
                        if (currentHiragana.Equals(hiragana))
                        {
                            IsExist = true;
                            break;                            
                        }                                                       
                    }

                    if (!IsExist)
                    {
                        IncorrectHiraganaAnswers.Add(hiragana);
                    }
                }
                else //If list is empty
                {
                    IncorrectHiraganaAnswers.Add(hiragana);
                }

                if (!IsExists)
                {
                    HiraganaAppearance.Add(hiragana);
                }
            }
            else if (!IsPractice && IsCorrect)
            {                
                if(!IsExists)
                {
                    HiraganaAppearance.Add(hiragana);
                }
            }
        }

        public string ReadCookie(string cookieName)
        {
            string cookieValue = Request.Cookies[cookieName];

            if (cookieValue != null)
                return cookieValue;
            else
                return null;
        }       
    }          
}