using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
namespace EasyHousingSolutionMVC.Models
{
    public class Seller
    {
        // [Required]
        [DisplayName("Seller ID")]
        public int SellerId { get; set; }
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        /* public User User { get; set; }*/
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Display(Name = "Date of Birth")]
        public DateOnly DateofBirth { get; set; }

        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; } = null!;
        [Display(Name = "Address")]
        public string Address { get; set; } = null!;
        [Display(Name = "State Name")]
        public int StateId { get; set; }
        [Display(Name = "City Name")]
        public int CityId { get; set; }


        [Display(Name = "Email Id")]
        public string EmailId { get; set; } = null!;

        /*public City City { get; set; }//Navition Property
        public State State { get; set; }*/

    }
}