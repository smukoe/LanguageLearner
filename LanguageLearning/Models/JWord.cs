using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageLearning.Models
{
    public class JWord
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }
        public string Kana { get; set; }

        [Display(Name = "Parts of Speech")]
        [DataType(DataType.Text)]
        public string PartsOfSpeech { get; set; }     
        public string Notes { get; set; }
    }

}