using Microsoft.EntityFrameworkCore;

namespace LanguageLearning.Models
{
    public class WordContext : DbContext
    {
        public WordContext(DbContextOptions<WordContext> options)
                : base(options)
        {
        }

        public DbSet<JWord> JWord { get; set; }
        public DbSet<KWord> KWord { get; set; } 
    }
}
