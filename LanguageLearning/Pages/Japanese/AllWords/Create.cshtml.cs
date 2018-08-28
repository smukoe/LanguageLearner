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
           
            if (IsDuplicateWordName(JWord.Name))
            {
                _context.JapaneseWord.Add(JWord);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            else
            {                
                //somehow return an error message
                
                return Page();
            }                       
        }

        private bool IsDuplicateWordName(string wordName)
        {
            //Each word name is assumed to be unique
            IQueryable<JapaneseWord> JWordQuery = from words in _context.JapaneseWord
                                                  select words;
            List<JapaneseWord> matchingWords = new List<JapaneseWord>();

            JWordQuery = JWordQuery.Where(w => w.Name == wordName);
            matchingWords = JWordQuery.ToList();

            if (matchingWords.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}