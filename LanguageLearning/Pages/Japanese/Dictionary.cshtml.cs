using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageLearning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LanguageLearning.Pages.Japanese
{   
    public class DictionaryModel : PageModel
    {
        private readonly WordContext _context;

        public DictionaryModel(WordContext context)
        {
            _context = context;
        }

        public List<JapaneseWord> ContainsUsersWord { get; set; }

        public void OnGet()
        {

        }

        public JsonResult OnGetSearchDictionary(string WordToSearch, string Kana)
        {
            ContainsUsersWord = new List<JapaneseWord>();
            ContainsUsersWord.Clear();

            if (!string.IsNullOrEmpty(Kana))
            {
                GetListOfWordsCaseKana(Kana);
            }

            if (!string.IsNullOrEmpty(WordToSearch))
            {
                GetListOfWords(WordToSearch);
            }                    
            return new JsonResult(ContainsUsersWord);
        }

        public void GetListOfWords(string WordToSearch)
        {            
            var AllWords = _context.JapaneseWord;
            
            //Checks if the word starts with the input
            foreach(JapaneseWord JWord in AllWords)
            {
                if(JWord.Name.StartsWith(WordToSearch, StringComparison.InvariantCultureIgnoreCase)) 
                {
                    ContainsUsersWord.Add(JWord);
                }
                else if(JWord.Definition.StartsWith(WordToSearch, StringComparison.InvariantCultureIgnoreCase))
                {
                    ContainsUsersWord.Add(JWord);
                }
            }

            //Secondary search if it can't find any words beginning with the input
            //Checks if the word contains the input
            foreach (JapaneseWord JWord in AllWords)
            {
                if (JWord.Name.CaseInsensitiveContains(WordToSearch))
                {
                    //Checks if the word already exists in the list
                    if (!ContainsUsersWord.Any(w => w.Name == JWord.Name))
                    {
                        ContainsUsersWord.Add(JWord);
                    }
                }
                else if (JWord.Definition.CaseInsensitiveContains(WordToSearch))
                {
                    if (!ContainsUsersWord.Any(w => w.Definition == JWord.Definition))
                    {
                        ContainsUsersWord.Add(JWord);
                    }
                }
            }

        }       

        public void GetListOfWordsCaseKana(string Kana)
        {
            var AllWords = _context.JapaneseWord;

            foreach (JapaneseWord JWord in AllWords)
            {
                if (JWord.Kana.StartsWith(Kana, StringComparison.InvariantCultureIgnoreCase))
                {
                    ContainsUsersWord.Add(JWord);
                }               
            }
        }
    }
}