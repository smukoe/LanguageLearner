using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LanguageLearning.Models;

namespace LanguageLearning.Pages.JDictionary
{
    public class EditModel : PageModel
    {
        private readonly LanguageLearning.Models.WordContext _context;

        public EditModel(LanguageLearning.Models.WordContext context)
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
            return _context.JWord.Any(e => e.ID == id);
        }
    }
}
