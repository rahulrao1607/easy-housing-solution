﻿using System;
using System.Collections.Generic;

namespace EasyHousingSolutionWebAPI.Models;

public partial class State
{
    public int StateId { get; set; }

    public string StateName { get; set; } = null!;

    //public virtual ICollection<City> Cities { get; set; } = new List<City>();

    //public virtual ICollection<Seller> Sellers { get; set; } = new List<Seller>();
}
