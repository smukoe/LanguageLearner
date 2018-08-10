using System.ComponentModel.DataAnnotations;

namespace LanguageLearning.Models
{
    public class Hiragana
    {
        public int ID { get; set; }
        [Required]
        public string Kana { get; set; }
        [Required]
        public string Romaji { get; set; }
    }
}
