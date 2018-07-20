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
    public class HiraganaIndexModel : PageModel
    {
        private readonly WordContext _context;

        public HiraganaIndexModel(WordContext context)
        {
            _context = context;
        }

        public IList<Hiragana> Hiragana { get; set; }
        public List<Hiragana> ListHiragana { get; set; }       

        public async Task OnGetAsync()
        {
            IQueryable<Hiragana> HiraganaQuery = from words in _context.Hiragana
                                                         select words;
                                                                                      
            ListHiragana = await HiraganaQuery.AsNoTracking().ToListAsync();                       
        }                      
    }
}
