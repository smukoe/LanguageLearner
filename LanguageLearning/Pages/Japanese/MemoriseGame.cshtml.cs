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

namespace LanguageLearning.Pages.Japanese
{       
    public class GameResult
    {
        public bool AnswerCheck { get; set; }
    }

    public class MemoriseGameModel : PageModel
    {
        private readonly WordContext _context;


        //Create Model object and pass in context
        public MemoriseGameModel(WordContext context)
        {
            //Store the context in a class property
            _context = context;
        }
                  
        //Variables for random word selection
        public JapaneseWord RandomWordData { get; private set; }

        //Variables for Word translate game       
        public JapaneseWord CorrectJWord { get; private set; }
      
        public JapaneseWord RandomDataSelection() //Random selection of data from the database
        {
            var AllWords = _context.JapaneseWord;
            int totalDataCount = AllWords.Count(); 
            Random randomNumber = new Random(); 
            int offset = randomNumber.Next(0, totalDataCount);

            return AllWords.Skip(offset).FirstOrDefault();                    
        }
            
        public void WritePersistentCookie(string setting, string settingValue) //Writes a persistent cookie
        {
            CookieOptions cookieOptions = new CookieOptions();
            CookieOptions WordSession = cookieOptions;
            WordSession.Expires = DateTime.Now.AddMinutes(5); //Temporarily set to 5 minutes, real expiry will be forever
            Response.Cookies.Append(setting, settingValue, WordSession);
        }

        public void ReadCookies()
        {
            string FirstTimeRun = Request.Cookies["HasGameStarted"];          

            if (FirstTimeRun == null)
            {               
                string CookieName = "HasGameStarted";
                string CookieValue = "0";
                WritePersistentCookie(CookieName, CookieValue);
            }
            else
            {
                
            }       
        }
                 
        public JsonResult OnGetJapanWord()
        {
            List<JapaneseWord> RandomWords = new List<JapaneseWord>();
            RandomWords.Clear();
                       
            while(RandomWords.Count < 4)
            {
                RandomWordData = RandomDataSelection();
                if (!RandomWords.Contains(RandomWordData))
                {
                    RandomWords.Add(RandomWordData);
                }                                
            }                                                          
            return new JsonResult(RandomWords);             
        }

        public JsonResult OnGetCheckAnswer(string Question, string Answer)
        {
            var AllWords = _context.JapaneseWord;
            foreach (JapaneseWord currentJapaneseWord in AllWords)
            {
                if (currentJapaneseWord.Name.Contains(Question))
                {
                    CorrectJWord = currentJapaneseWord;
                }
            }

            GameResult IsTheAnswerCorrect = new GameResult();
            if (CorrectJWord.Definition.Contains(Answer))
            {
                IsTheAnswerCorrect.AnswerCheck = true;
            }
            else
            {
                IsTheAnswerCorrect.AnswerCheck = false;
            }
            return new JsonResult(IsTheAnswerCorrect);
        }              
    }       
}