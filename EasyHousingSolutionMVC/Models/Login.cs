using System.ComponentModel.DataAnnotations;

namespace EasyHousingSolutionMVC.Models
{
    public class Login
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "UserName Can't be empty")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password Can't be empty")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        
    }
}
