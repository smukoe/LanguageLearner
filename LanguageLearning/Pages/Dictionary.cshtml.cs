using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageLearning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LanguageLearning.Pages
{   
    public class DictionaryModel : PageModel
    {
        private readonly WordContext _context;

        public DictionaryModel(WordContext context)
        {
            _context = context;
        }

        public List<JapaneseWord> ContainsUsersJWord { get; set; }
        public List<KoreanWord> ContainsUsersKWord { get; set; }
        
        public JsonResult OnGetSearchJapaneseInput(string WordToSearch, string Kana, string InputLanguage)
        {
            ContainsUsersJWord = new List<JapaneseWord>();
            ContainsUsersJWord.Clear();
            if(InputLanguage == "English")
            {
                if (!string.IsNullOrEmpty(Kana))
                {
                    GetJapaneseWordsFromJapanese(Kana);
                }

                if (!string.IsNullOrEmpty(WordToSearch))
                {
                    GetJapaneseWordsFromEnglish(WordToSearch);
                }
            }
            else if(InputLanguage == "Japanese")
            {
                if (!string.IsNullOrEmpty(WordToSearch))
                {
                    GetJapaneseWordsFromJapanese(WordToSearch);
                }
            }
            
            return new JsonResult(ContainsUsersJWord);
        }

        public JsonResult OnGetSearchKoreanInput()
        {
            ContainsUsersKWord = new List<KoreanWord>();
            ContainsUsersKWord.Clear();

            //Get korean words

            return new JsonResult(ContainsUsersKWord);
        }

        public void GetJapaneseWordsFromEnglish(string WordToSearch)
        {
            var AllWords = _context.JapaneseWord;                      
                    //Checks if the word starts with the input
                    foreach (JapaneseWord JWord in AllWords)
                    {
                        if (JWord.Name.StartsWith(WordToSearch, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ContainsUsersJWord.Add(JWord);
                        }
                        else if (JWord.Definition.StartsWith(WordToSearch, StringComparison.InvariantCultureIgnoreCase))
                        {
                            ContainsUsersJWord.Add(JWord);
                        }
                    }

                    //Secondary search if it can't find any words beginning with the input
                    //Checks if the word contains the input
                    foreach (JapaneseWord JWord in AllWords)
                    {
                        if (JWord.Name.CaseInsensitiveContains(WordToSearch))
                        {
                            //Checks if the word already exists in the list
                            if (!ContainsUsersJWord.Any(w => w.Name == JWord.Name))
                            {
                                ContainsUsersJWord.Add(JWord);
                            }
                        }
                        else if (JWord.Definition.CaseInsensitiveContains(WordToSearch))
                        {
                            if (!ContainsUsersJWord.Any(w => w.Definition == JWord.Definition))
                            {
                                ContainsUsersJWord.Add(JWord);
                            }
                        }
                    }                                   
        }       

        public void GetJapaneseWordsFromJapanese(string JapaneseInput)
        {
            var AllWords = _context.JapaneseWord;

            foreach (JapaneseWord JWord in AllWords)
            {
                if (JWord.Kana.StartsWith(JapaneseInput))
                {
                    ContainsUsersJWord.Add(JWord);
                }      
                else if (JWord.Name.StartsWith(JapaneseInput))
                {
                    ContainsUsersJWord.Add(JWord);
                }
            }

            foreach (JapaneseWord JWord in AllWords)
            {
                if (JWord.Kana.CaseInsensitiveContains(JapaneseInput))
                {
                    //Checks if the word already exists in the list
                    if (!ContainsUsersJWord.Any(w => w.Kana == JWord.Kana) && !ContainsUsersJWord.Any(w => w.Name == JWord.Name))
                    {
                        ContainsUsersJWord.Add(JWord);
                    }
                }
                else if (JWord.Name.CaseInsensitiveContains(JapaneseInput))
                {
                    if (!ContainsUsersJWord.Any(w => w.Name == JWord.Name))
                    {
                        ContainsUsersJWord.Add(JWord);
                    }
                }
            }
        }
    }
}