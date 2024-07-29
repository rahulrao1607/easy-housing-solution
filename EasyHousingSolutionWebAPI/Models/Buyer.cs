using System;
using System.Collections.Generic;

namespace EasyHousingSolutionWebAPI.Models;

public partial class Buyer
{
    public int BuyerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public DateOnly DateofBirth { get; set; }

    public string PhoneNo { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    //public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
