using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageLearning.Models
{
    public class JapaneseWord
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Definition { get; set; }
        [Required]
        public string Kana { get; set; }

        [Display(Name = "Parts of Speech")]
        [DataType(DataType.Text)]
        public string PartsOfSpeech { get; set; }     
        public string Notes { get; set; }
    }

}