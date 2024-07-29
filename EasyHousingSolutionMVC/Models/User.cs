using System;
using System.Collections.Generic;

namespace EasyHousingSolutionMVC.Models;

public partial class User
{
    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? UserType { get; set; }

    public string Email { get; set; } = null!;

    //public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();
}
