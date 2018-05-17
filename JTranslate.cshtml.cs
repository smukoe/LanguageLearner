using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using LanguageLearning.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.EntityFrameworkCore;

namespace LanguageLearning.Pages
{

    public static class Extension 
    {
        public static bool CaseInsensitiveContains(this string wordToTranslate, string value, StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        //Method used to ignore case when comparing strings
        {
            return wordToTranslate.IndexOf(value, stringComparison) >= 0;
        }
    }

    public class FirstTimeRun
    {
        public bool FirstTime = true;       
    }
    
    public class JTranslateModel : PageModel
    {
        private readonly WordContext _context;
        

        //Create Model objext and pass in context
        public JTranslateModel(WordContext context)
        {
            //Store the context in a class property
            _context = context;           
        }
                
        public JWord SelectedWord { get; private set; }
        public DbSet<JWord> AllWords { get; private set; }
        public IList<JWord> JWord { get; set; }

        public string EngOrJap { get; private set; }
        public string CorrectAnswer { get; private set; } = "Don't know yet";

        public JWord RandomWordData { get; private set; }
        public string RandomWordSelected { get; private set; }
        public bool NextRandomWord { get; private set; } = false;
        public int CurrentScore = 0;
       
        public async Task OnGetAsync(string wordToTranslate, string wordTranslateGuess)
        {            
            IQueryable<string> WordQuery = from w in _context.JWord
                                           select w.Name;
           
            DbSet<JWord> allWords = _context.JWord;

            var DBWords = from p in _context.JWord
                          select p;

            if (!String.IsNullOrEmpty(wordToTranslate)) //Translates the user input
            {
                foreach (JWord currentWord in allWords) //Cycles through the database
                {
                    if (currentWord.Name.CaseInsensitiveContains(wordToTranslate)) //Comparison of database and user input, ignoring cases
                    {
                        SelectedWord = currentWord;
                        EngOrJap = SelectedWord.Definition;
                    }

                    if (currentWord.Definition.CaseInsensitiveContains(wordToTranslate))
                    {
                        SelectedWord = currentWord;
                        EngOrJap = SelectedWord.Name;
                    }
                }
            }

            if (FirstTimeRun.FirstTime == true)
            {
                RandomWordData = RandomDataSelect();
                RandomWordSelected = RandomWordData.Name;                
            }           

            if (!String.IsNullOrEmpty(wordTranslateGuess)) 
            {                
                if (RandomWordData.Definition.CaseInsensitiveContains(wordTranslateGuess)) 
                {
                    CorrectAnswer = "Yes";
                    RandomWordData = RandomDataSelect(); 
                    RandomWordSelected = RandomWordData.Name;
                    CurrentScore += 1;
                }                               
                else
                {
                    CorrectAnswer = "No";
                }
            }
            JWord = await DBWords.ToListAsync();
        }
        
        public JWord RandomDataSelect() //Random selection of data from the database
        {
            AllWords = _context.JWord; 
            int totalDataCount = AllWords.Count(); 
            Random r = new Random(); 
            int offset = r.Next(0, totalDataCount); 

            return AllWords.Skip(offset).FirstOrDefault();                                   
        }
             
        public void Translate(string wordToTranslate)
        {

        }


    }   
}