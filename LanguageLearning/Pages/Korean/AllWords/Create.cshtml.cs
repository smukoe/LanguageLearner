using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LanguageLearning.Models;

namespace LanguageLearning.Pages.Korean.AllWords
{
    public class KoreanCreateModel : PageModel
    {
        private readonly WordContext _context;

        public KoreanCreateModel(WordContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public KoreanWord KWord { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            _context.KoreanWord.Add(KWord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        
    }
}