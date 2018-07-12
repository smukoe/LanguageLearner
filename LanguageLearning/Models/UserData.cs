using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageLearning.Models
{
    public class UserData
    {
        public int ID { get; set; }

        [Display(Name = "Username")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
