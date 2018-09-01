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
    public class JapaneseDeleteModel : PageModel
    {
        private readonly WordContext _context;

        public JapaneseDeleteModel(WordContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JWord = await _context.JapaneseWord.FindAsync(id);

            if (JWord != null)
            {
                _context.JapaneseWord.Remove(JWord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
