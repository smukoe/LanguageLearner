using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LanguageLearning.Models;

namespace LanguageLearning.Pages.Korean.AllWords
{
    public class KoreanDeleteModel : PageModel
    {
        private readonly WordContext _context;

        public KoreanDeleteModel(WordContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            KWord = await _context.KoreanWord.FindAsync(id);

            if (KWord != null)
            {
                _context.KoreanWord.Remove(KWord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
