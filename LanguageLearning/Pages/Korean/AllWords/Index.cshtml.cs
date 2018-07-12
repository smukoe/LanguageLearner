using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LanguageLearning.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearning.Pages.Korean.AllWords
{
    public class KoreanIndexModel : PageModel
    {
        private readonly WordContext _context;

        public KoreanIndexModel(WordContext context)
        {
            _context = context;
        }

        public IList<KoreanWord> KoreanWord { get; set; }
       
        public string SortDefinition { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
               
        public List<KoreanWord> ListAssign { get; set; }       

        public async Task OnGetAsync(string searchWordName, string sortOrder)
        {
            IQueryable<KoreanWord> KoreanWordQuery = from words in _context.KoreanWord
                                                         select words;
                     
            if (!String.IsNullOrEmpty(searchWordName))
            {
                // Lists all JWord whose name or definition contains the search string
                KoreanWordQuery = KoreanWordQuery.Where(filteredWords => filteredWords.Name.Contains(searchWordName) || filteredWords.Definition.Contains(searchWordName));                
            }
                           
            //Sort by definition            
            SortDefinition = String.IsNullOrEmpty(sortOrder) ? "definition_desc" : "";          

            switch (sortOrder)
            {               
                case "definition_desc":
                    KoreanWordQuery = KoreanWordQuery.OrderByDescending(sortedWords => sortedWords.Definition);                    
                    break;
                default:
                    KoreanWordQuery = KoreanWordQuery.OrderBy(sortedWords => sortedWords.Definition);                    
                    break;
            }
            ListAssign = await KoreanWordQuery.AsNoTracking().ToListAsync();                       
        }                      
    }
}
