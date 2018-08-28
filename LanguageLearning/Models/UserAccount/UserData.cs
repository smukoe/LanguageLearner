using System.ComponentModel.DataAnnotations;

namespace LanguageLearning.Models.UserAccount
{
    public class UserData
    {
        public int ID { get; set; }

        [Required]

        [StringLength(15, MinimumLength = 6)]
        [Display(Name = "Username")]
        [DataType(DataType.Text)]        
        [RegularExpression(@"^[A-Za-z]+[a-zA-Z0-9]*$")]
        public string UserName { get; set; }

        [Required]        
        [DataType(DataType.Password)]        
        public string Password { get; set; }
        public string StringifiedSalt { get; set; }
    }
}
