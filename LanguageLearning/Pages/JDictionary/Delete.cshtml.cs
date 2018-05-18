using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LanguageLearning.Models;

namespace LanguageLearning.Pages.JDictionary
{
    public class DeleteModel : PageModel
    {
        private readonly LanguageLearning.Models.WordContext _context;

        public DeleteModel(LanguageLearning.Models.WordContext context)
        {
            _context = context;
        }

        [BindProperty]
        public JWord JWord { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            JWord = await _context.JWord.SingleOrDefaultAsync(m => m.ID == id);

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

            JWord = await _context.JWord.FindAsync(id);

            if (JWord != null)
            {
                _context.JWord.Remove(JWord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
