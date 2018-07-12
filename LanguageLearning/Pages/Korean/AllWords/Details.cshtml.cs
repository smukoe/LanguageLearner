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
    public class KoreanDetailsModel : PageModel
    {
        private readonly WordContext _context;

        public KoreanDetailsModel(WordContext context)
        {
            _context = context;
        }

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
    }
}
