using LanguageLearning.Interfaces;
using System;
using System.Linq;

namespace LanguageLearning.Models.Languages
{
    public class WordQuizService : IWordQuizService
    {
        private readonly WordContext _context;             
        
        public WordQuizService(WordContext wordContext)
        {
            _context = wordContext;           
        }
        
        public JapaneseWord RandomJapaneseWordSelect()
        {            
            int totalDataCount = _context.JapaneseWord.Count();
            Random randomNumber = new Random();
            int offset = randomNumber.Next(0, totalDataCount);

            return _context.JapaneseWord.Skip(offset).FirstOrDefault();
        }

        public Hiragana RandomHiraganaSelect()
        {
            int totalDataCount = _context.Hiragana.Count();
            Random randomNumber = new Random();
            int offset = randomNumber.Next(0, totalDataCount);

            return _context.Hiragana.Skip(offset).FirstOrDefault();
        }       
    }
}
