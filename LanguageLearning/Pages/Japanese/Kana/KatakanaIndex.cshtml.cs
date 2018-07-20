using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LanguageLearning.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearning.Pages.Japanese.Kana
{
    public class KatakanaIndexModel : PageModel
    {
        private readonly WordContext _context;

        public KatakanaIndexModel(WordContext context)
        {
            _context = context;
        }

        public IList<Katakana> Katakana { get; set; }
        public List<Katakana> ListKatakana { get; set; }       

        public async Task OnGetAsync()
        {
            IQueryable<Katakana> HiraganaQuery = from words in _context.Katakana
                                                         select words;
                                                                                      
            ListKatakana = await HiraganaQuery.AsNoTracking().ToListAsync();                       
        }                      
    }
}
