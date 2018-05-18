using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LanguageLearning.Models;

namespace LanguageLearning.Pages.JDictionary
{
    public class CreateModel : PageModel
    {
        private readonly LanguageLearning.Models.WordContext _context;

        public CreateModel(LanguageLearning.Models.WordContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public JWord JWord { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.JWord.Add(JWord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}