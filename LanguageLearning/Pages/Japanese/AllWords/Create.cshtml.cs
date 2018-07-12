using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LanguageLearning.Models;

namespace LanguageLearning.Pages.Japanese.AllWords
{
    public class JapaneseCreateModel : PageModel
    {
        private readonly WordContext _context;

        public JapaneseCreateModel(WordContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public JapaneseWord JWord { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            _context.JapaneseWord.Add(JWord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        
    }
}