using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LanguageLearning.Models;

namespace LanguageLearning.Pages.Japanese.AllWords
{
    public class JapaneseDetailsModel : PageModel
    {
        private readonly WordContext _context;

        public JapaneseDetailsModel(WordContext context)
        {
            _context = context;
        }

        public JapaneseWord JWord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JWord = await _context.JapaneseWord.SingleOrDefaultAsync(m => m.ID == id);

            if (JWord == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
