﻿using System;
using System.ComponentModel.DataAnnotations;

namespace LanguageLearning.Models
{
    public class KWord
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Definition { get; set; }        

        [Display(Name = "Parts of Speech")]
        [DataType(DataType.Text)]
        public string PartsOfSpeech { get; set; }
        public string Notes { get; set; }
    }

}