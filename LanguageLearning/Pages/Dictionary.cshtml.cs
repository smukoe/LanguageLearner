using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LanguageLearning.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using LanguageLearning.Enums;
using LanguageLearning.Interfaces;

namespace LanguageLearning.Pages
{   
    public class DictionaryModel : PageModel
    {
        private readonly WordContext _context;
        private readonly IJwtValidation _jwtValidation;
        private readonly ILanguageOptions _languageOptions;
        public DictionaryModel(IJwtValidation jwtValidation, 
                                   WordContext context,
                                   ILanguageOptions languageOptions)
        {
            _context = context;
            _jwtValidation = jwtValidation;
            _languageOptions = languageOptions;
        }
                 

        public JsonResult OnGetClaimsDictionary()
        {            
            string userToken = ReadCookie("Token");

            if (String.IsNullOrEmpty(userToken))
            {
                return new JsonResult("Invalid");
            }
            else
            {
                JObject JSONJwt = _jwtValidation.ParseTokenToJSON(userToken);
                string username = _jwtValidation.JSONGetValue(JSONJwt, "sub");
                return new JsonResult(JSONJwt);
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

        public JsonResult OnGetSearchJapaneseInput(string WordToSearch, string Kana, string InputLanguage)
        {
            IEnumerable<JapaneseWord> matchingWords = new List<JapaneseWord>();
            Language language = _languageOptions.MapStringToEnum(InputLanguage);

            if(language == Language.English && !String.IsNullOrEmpty(Kana) && !String.IsNullOrEmpty(WordToSearch))
            {                
                matchingWords = matchingWords.Concat(GetJapaneseWordsFromEnglishOrKanaEquivalent(WordToSearch, Kana));
            }          

            if(language == Language.Japanese && !String.IsNullOrEmpty(WordToSearch))
            {
                matchingWords = matchingWords.Concat(GetJapaneseWordsFromJapaneseInput(WordToSearch));
            }

            return new JsonResult(matchingWords);
        }        

        private List<JapaneseWord> GetJapaneseWordsFromEnglishOrKanaEquivalent(string englishInput, string kana)
        {
            var AllWords = _context.JapaneseWord;
            List<JapaneseWord> matchingWords = new List<JapaneseWord>();

            foreach (JapaneseWord JWord in AllWords)
            {               
                //Prioritise displaying kana first
                if (JWord.Kana.StartsWith(kana))
                {
                    matchingWords.Add(JWord);
                }
                else if (JWord.Definition.StartsWith(englishInput, StringComparison.InvariantCultureIgnoreCase))
                {
                    matchingWords.Add(JWord);
                }

                //English
                if (JWord.Definition.CaseInsensitiveContains(englishInput) && !matchingWords.Any(list => list.Definition == JWord.Definition))
                {
                    matchingWords.Add(JWord);
                }

                //Kana                
                if (JWord.Kana.Contains(kana) && !matchingWords.Any(list => list.Kana == JWord.Kana))
                {
                    matchingWords.Add(JWord);
                }
            }                       
            return matchingWords;
        }                     

        private List<JapaneseWord> GetJapaneseWordsFromJapaneseInput(string japaneseInput)
        {
            var AllWords = _context.JapaneseWord;
            List<JapaneseWord> matchingWords = new List<JapaneseWord>();

            foreach (JapaneseWord JWord in AllWords)
            {
                if (JWord.Kana.StartsWith(japaneseInput))
                {
                    matchingWords.Add(JWord);
                }      
                else if (JWord.Name.StartsWith(japaneseInput))
                {
                    matchingWords.Add(JWord);
                }

                if (JWord.Kana.Contains(japaneseInput) && !matchingWords.Any(list => list.Kana == JWord.Kana) && !matchingWords.Any(list => list.Name == JWord.Name))
                {
                    matchingWords.Add(JWord);
                }
                else if (JWord.Name.Contains(japaneseInput) && !matchingWords.Any(list => list.Name == JWord.Name))
                {
                    matchingWords.Add(JWord);
                }
            }
            return matchingWords;
        }
        
        public JsonResult OnGetSearchKoreanInput(string WordToSearch, string InputLanguage)
        {
            IEnumerable<KoreanWord> matchingWords = new List<KoreanWord>();
            Language language = _languageOptions.MapStringToEnum(InputLanguage);

            if (language == Language.English && !String.IsNullOrEmpty(WordToSearch))
            {
                matchingWords = matchingWords.Concat(GetKoreanWordsFromEnglishInput(WordToSearch));
            }
            else if (language == Language.Korean && !String.IsNullOrEmpty(WordToSearch))
            {
                matchingWords = matchingWords.Concat(GetKoreanWordsFromKoreanInput(WordToSearch));
            }

            return new JsonResult(matchingWords);
        }

        private List<KoreanWord> GetKoreanWordsFromEnglishInput(string englishInput)
        {
            var AllWords = _context.KoreanWord;
            List<KoreanWord> matchingWords = new List<KoreanWord>();
            
            foreach (KoreanWord KWord in AllWords)
            {              
                if (KWord.Definition.StartsWith(englishInput, StringComparison.InvariantCultureIgnoreCase))
                {
                    matchingWords.Add(KWord);
                }

                if (KWord.Definition.CaseInsensitiveContains(englishInput) && !matchingWords.Any(w => w.Definition == KWord.Definition))
                {
                    matchingWords.Add(KWord);
                }
            }                      
            return matchingWords;
        }

        private List<KoreanWord> GetKoreanWordsFromKoreanInput(string koreanInput)
        {
            var AllWords = _context.KoreanWord;
            List<KoreanWord> matchingWords = new List<KoreanWord>();

            foreach (KoreanWord KWord in AllWords)
            {                
                if (KWord.Name.StartsWith(koreanInput))
                {
                    matchingWords.Add(KWord);
                }
               
                if (KWord.Name.CaseInsensitiveContains(koreanInput) && !matchingWords.Any(list => list.Name == KWord.Name))
                {
                    matchingWords.Add(KWord);
                }
            }
            return matchingWords;
        }
    }
}