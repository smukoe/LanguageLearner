using LanguageLearning.Models.LanguageQuiz;
using LanguageLearning.Models.LanguageQuiz.WordAppearances;
using LanguageLearning.Models.UserAccount;
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
        public DbSet<Hiragana> Hiragana { get; set; }
        public DbSet<Katakana> Katakana { get; set; }
        public DbSet<KoreanWord> KoreanWord { get; set; } 

        public DbSet<UserData> UserData { get; set; }

        public DbSet<QuizSessionModel> QuizSessionModel { get; set; }

        public DbSet<JapaneseWordAppearance> JapaneseWordAppearances { get; set; }
        public DbSet<KoreanWordAppearance> KoreanWordAppearances { get; set; }
        public DbSet<HiraganaAppearance> HiraganaAppearances { get; set; }
        public DbSet<KatakanaAppearance> KatakanaAppearances { get; set; }
    }
}
