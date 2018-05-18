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
    public class DetailsModel : PageModel
    {
        private readonly LanguageLearning.Models.WordContext _context;

        public DetailsModel(LanguageLearning.Models.WordContext context)
        {
            _context = context;
        }

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
    }
}
