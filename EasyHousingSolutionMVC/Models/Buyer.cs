using System.ComponentModel.DataAnnotations;

namespace EasyHousingSolutionMVC.Models
{
    public class Buyer
    {
        public int BuyerId { get; set; }

        [Required(ErrorMessage = "First Name is Mandatory")]
        [Length(3, 15, ErrorMessage = "Name must be min 3 char and max 15 char.")]
        [RegularExpression(@"^[A-Za-z]+$", ErrorMessage = "First Name cannot contain numbers.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;



		[Display(Name = "Last Name")]
        public string LastName { get; set; }



		[Required(ErrorMessage = "Date of Birth is Mandatory")]
		[Display(Name = "Date of Birth")]
        public DateOnly DateofBirth { get; set; }

        [Required]
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; } = null!;

		[Required]
        [Display(Name = "Email Address")]
        public string EmailId { get; set; } = null!;
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; } = null!;
    }
}