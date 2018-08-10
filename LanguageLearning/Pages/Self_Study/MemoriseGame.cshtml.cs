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

    public class MemoriseGameModel : PageModel
    {
        private readonly WordContext _context;
        private readonly MemoriseGame _memoriseGame;

        //Create Model object and pass in context
        public MemoriseGameModel(WordContext context, MemoriseGame memoriseGame)
        {
            //Store the context in a class property
            _context = context;
            _memoriseGame = memoriseGame;
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
                 
        public JsonResult OnGetSetGameType(string mainLanguage, bool isPractice)
        {            
            WordCount wordCount = new WordCount();            

            IsPractice = isPractice;
            switch (mainLanguage)
            {
                case "Japanese":
                    wordCount.AllWordsCount = _context.JapaneseWord.Count();                   
                    break;
                case "Hiragana":
                    wordCount.AllWordsCount = _context.Hiragana.Count();
                    
                    //Clear list to start a fresh game
                    if (HiraganaAppearance.Any())
                    {
                        HiraganaAppearance.Clear();
                    }         
                    
                    if (IncorrectHiraganaAnswers.Any())
                    {
                        IncorrectHiraganaAnswers.Clear();
                    }                                      
                    break;
                case "Katakana":
                    wordCount.AllWordsCount = _context.Katakana.Count();                 
                    break;
                case "Korean":
                    wordCount.AllWordsCount = _context.KoreanWord.Count();                    
                    break;               
            }
                                  
            return new JsonResult(wordCount);
        }

        public JsonResult OnGetCheckGameStatus(int TotalWordCount)
        {
            GameEndState Game = new GameEndState();
            //If all words have been selected
            if (TotalWordCount != 0 && TotalWordCount == WordAppearanceCount)
            {
                Game.IsFinished = true;                
            }
            else
            {
                Game.IsFinished = false;
            }
            return new JsonResult(Game);
        }             

        public JsonResult OnGetJapanWord()
        {
            List<JapaneseWord> RandomJWords = new List<JapaneseWord>();
            RandomJWords.Clear();
            
            while(RandomJWords.Count < 4)
            {
                RandomJapWord = _memoriseGame.RandomJapaneseWordSelect();
                if (!RandomJWords.Contains(RandomJapWord))
                {
                    RandomJWords.Add(RandomJapWord);
                }                                
            }                                                          
            return new JsonResult(RandomJWords);             
        }

        public JsonResult OnGetHiragana()
        {
            List<Hiragana> RandomHiraganaWords = new List<Hiragana>();
            RandomHiraganaWords.Clear();
            if (HiraganaAppearance.Safe().Any())
            {
                WordAppearanceCount = HiraganaAppearance.Count();
            }
            else
            {
                WordAppearanceCount = 0;
            }
            
            while (RandomHiraganaWords.Count < 4)
            {
                RandomHiragana = _memoriseGame.RandomHiraganaSelect();
                if (!RandomHiraganaWords.Any() && !CheckHiraganaOccurance(RandomHiragana)) //For the first word added to the list because it is the display word
                {
                    RandomHiraganaWords.Add(RandomHiragana);
                }
                else if (!RandomHiraganaWords.Contains(RandomHiragana)) //All other words
                {
                    RandomHiraganaWords.Add(RandomHiragana);
                }
            }
            return new JsonResult(RandomHiraganaWords);
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
    }          
}