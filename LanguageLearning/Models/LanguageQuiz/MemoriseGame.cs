using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Models.Languages
{
    public class MemoriseGame
    {
        private WordContext _wordContext;             
        
        public MemoriseGame(WordContext wordContext)
        {
            _wordContext = wordContext;
            HiraganaAppearance = new List<Hiragana>();
            IncorrectHiraganaAnswers = new List<Hiragana>();
        }

        public JapaneseWord RandomJapaneseWordSelect()
        {            
            int totalDataCount = _wordContext.JapaneseWord.Count();
            Random randomNumber = new Random();
            int offset = randomNumber.Next(0, totalDataCount);

            return _wordContext.JapaneseWord.Skip(offset).FirstOrDefault();
        }

        public Hiragana RandomHiraganaSelect()
        {
            int totalDataCount = _wordContext.Hiragana.Count();
            Random randomNumber = new Random();
            int offset = randomNumber.Next(0, totalDataCount);

            return _wordContext.Hiragana.Skip(offset).FirstOrDefault();
        }

        public List<Hiragana> HiraganaAppearance;
        public List<Hiragana> IncorrectHiraganaAnswers;
    }
}
