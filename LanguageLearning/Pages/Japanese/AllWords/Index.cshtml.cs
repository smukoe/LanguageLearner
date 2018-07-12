using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LanguageLearning.Models;
using Microsoft.AspNetCore.Mvc;

namespace LanguageLearning.Pages.Japanese.AllWords
{
    public class JapaneseIndexModel : PageModel
    {
        private readonly WordContext _context;

        public JapaneseIndexModel(WordContext context)
        {
            _context = context;
        }

        public IList<JapaneseWord> JapaneseWord { get; set; }
        public SelectList PartsOfSpeech { get; set; }       
        public string WordPartsOfSpeech { get; set; }
       
        public string SortDefinition { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
               
        public List<JapaneseWord> ListAssign { get; set; }       

        public async Task OnGetAsync(string searchWordName, string sortOrder)
        {
            IQueryable<JapaneseWord> JapaneseWordQuery = from words in _context.JapaneseWord
                                                         select words;
                     
            if (!String.IsNullOrEmpty(searchWordName))
            {
                // Lists all JWord whose name or definition contains the search string
                JapaneseWordQuery = JapaneseWordQuery.Where(filteredWords => filteredWords.Name.Contains(searchWordName) || filteredWords.Definition.Contains(searchWordName));                
            }
                           
            //Sort by definition            
            SortDefinition = String.IsNullOrEmpty(sortOrder) ? "definition_desc" : "";          

            switch (sortOrder)
            {               
                case "definition_desc":
                    JapaneseWordQuery = JapaneseWordQuery.OrderByDescending(sortedWords => sortedWords.Definition);                    
                    break;
                default:
                    JapaneseWordQuery = JapaneseWordQuery.OrderBy(sortedWords => sortedWords.Definition);                    
                    break;
            }
            ListAssign = await JapaneseWordQuery.AsNoTracking().ToListAsync();                       
        }                      
    }
}
