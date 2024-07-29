using System;
using System.Collections.Generic;

namespace EasyHousingSolutionMVC.Models;

public partial class City
{
    public int CityId { get; set; }

    public string CityName { get; set; } = null!;

    public int StateId { get; set; }

    //public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();

    //public virtual State State { get; set; } = null!;
}
