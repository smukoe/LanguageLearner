using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LanguageLearning.Models;

namespace LanguageLearning.Pages.JDictionary
{
    public class IndexModel : PageModel
    {
        private readonly WordContext _context;

        public IndexModel(WordContext context)
        {
            _context = context;
        }

        public IList<JWord> JWord { get; set; }
        public SelectList POS { get; set; }
        public string WordPOS { get; set; }

        public async Task OnGetAsync(string wordPOS, string searchWName, string searchWDef)
        {
            IQueryable<string> POSQuery = from w in _context.JWord
                                            orderby w.PartsOfSpeech
                                            select w.PartsOfSpeech;

            var words = from w in _context.JWord
                        select w;

            if (!String.IsNullOrEmpty(searchWName))
            {
                words = words.Where(s => s.Name.Contains(searchWName));
            }

            if (!String.IsNullOrEmpty(searchWDef))
            {
                words = words.Where(f => f.Definition.Contains(searchWDef));
            }

            if (!String.IsNullOrEmpty(wordPOS))
            {
                words = words.Where(x => x.PartsOfSpeech == wordPOS);
            }
            POS = new SelectList(await POSQuery.Distinct().ToListAsync());
            JWord = await words.ToListAsync();
        }
    }
}
