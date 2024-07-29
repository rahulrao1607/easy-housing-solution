using System;
using System.Collections.Generic;

namespace EasyHousingSolutionWebAPI.Models;

public partial class Seller
{
    public int SellerId { get; set; }

    public string UserName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateOnly DateofBirth { get; set; }

    public string PhoneNo { get; set; } = null!;

    public string Address { get; set; } = null!;

    public int StateId { get; set; }

    public int CityId { get; set; }

    public string EmailId { get; set; } = null!;

    //public virtual City City { get; set; } = null!;

    //public virtual ICollection<Property> Properties { get; set; } = new List<Property>();

    //public virtual State State { get; set; } = null!;

    //public virtual User UserNameNavigation { get; set; } = null!;
}
