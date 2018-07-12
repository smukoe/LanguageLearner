using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanguageLearning.Models;

namespace LanguageLearning.Pages.Japanese.AllWords
{
    public class JapaneseEditModel : PageModel
    {
        private readonly WordContext _context;

        public JapaneseEditModel(WordContext context)
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(JWord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JWordExists(JWord.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool JWordExists(int id)
        {
            return _context.JapaneseWord.Any(e => e.ID == id);
        }
    }
}
