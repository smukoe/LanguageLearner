using Microsoft.EntityFrameworkCore;

namespace LanguageLearning.Models
{
    public class WordContext : DbContext
    {
        public WordContext(DbContextOptions<WordContext> options)
                : base(options)
        {
        }

        public DbSet<JapaneseWord> JapaneseWord { get; set; }        
        public DbSet<KoreanWord> KoreanWord { get; set; } 
        public DbSet<UserData> UserData { get; set; }
    }
}
