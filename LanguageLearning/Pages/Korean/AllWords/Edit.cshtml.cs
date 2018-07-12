using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanguageLearning.Models;

namespace LanguageLearning.Pages.Korean.AllWords
{
    public class KoreanEditModel : PageModel
    {
        private readonly WordContext _context;

        public KoreanEditModel(WordContext context)
        {
            _context = context;
        }

        [BindProperty]
        public KoreanWord KWord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KWord = await _context.KoreanWord.SingleOrDefaultAsync(m => m.ID == id);

            if (KWord == null)
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

            _context.Attach(KWord).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KWordExists(KWord.ID))
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

        private bool KWordExists(int id)
        {
            return _context.JapaneseWord.Any(e => e.ID == id);
        }
    }
}
